using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class ClickObjectBehaviour : MonoBehaviour
{
    [HideInInspector]public ClickObject data;
    public UnityEvent onMouseDownEvent;
    public UnityEvent onInputFreezed;
    private int _life = -1;
    
    // Start is called before the first frame update

    public void Initialize(ClickObject _data)
    {
        data = _data;
        var obj = GameObject.Instantiate(data.prefab, this.gameObject.transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;

        this.data = _data;
        _life = data.clicksToDestroy;

        StartLife();
    }

    public void StartLife()
    {
        StartCoroutine(Lifetime());
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(data.lifespan);
        RewardManager.Instance.LoseReward(data);
        //can be made a pool of objects.
        ClickObjectFactory.Instance.Remove(this);
    }

    public void OnMouseDown()
    {
        onMouseDownEvent.Invoke();

        if (!GameManager.canClick)
        {
            onInputFreezed.Invoke();
            return;
        }

        _life--;
        if (_life <= 0)
        {
            RewardManager.Instance.GrantReward(data);
            this.GetComponent<Collider>().enabled = false;
        }
    }

    public void ProcessClick()
    {
        if (_life <= 0)
        {
            //can be made a pool of objects.
            ClickObjectFactory.Instance.Remove(this);
        }
    }
}
