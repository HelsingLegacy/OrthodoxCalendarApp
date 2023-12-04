using UnityEngine;

namespace CodeBase.UI.ContentFiller
{
  public abstract class ContentParent : MonoBehaviour
  {
    [SerializeField] private GameObject _parent;

    public virtual GameObject ParentObject()
    {
      return _parent;
    }
  }
}