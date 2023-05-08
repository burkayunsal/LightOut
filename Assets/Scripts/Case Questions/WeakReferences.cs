using UnityEngine;

/*
 * 
 * The SomeBehaviour class below holds a serialized reference to another MonoBehaviour which is in the same scene.
 * On Start, it performs some operations that are dependent on this OtherBehaviour.
 *
 * Later in development, these two behaviours were separated and were moved to different scenes.
 * The dependency still remains, therefore SomeBehaviour class should somehow get the reference of OtherBehaviour.
 * In the future, there can be more behaviours that are dependent each other. There can be cyclic dependencies in some cases.
 *
 * Implement your simple and generalized solution to this problem, so that similar problems that can be encountered in the future can be solved easily. 
 * 
 * Make sure you describe your code and intentions clearly.
 * 
 */

public class SomeBehaviour : MonoSingleton<SomeBehaviour>
{
    // SomeBehavior is created in the first scene and even if the scene changes, SomeBehaviour does not destroy, thanks to DontDestroyObLoad method, and does not lose any reference.
    // For multiple references, we can use [Inject] by using Zenject 
    
    public MonoBehaviour OtherBehaviour;

    private void Start()
    {
        // Operations dependent on OtherBehaviour
    }
}


public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T I;

    public virtual void Awake()
    {
        if(I != null)
        {
            Destroy(gameObject);
        }
        else
        {
            I = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }

}