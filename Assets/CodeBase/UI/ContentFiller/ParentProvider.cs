using UnityEngine;

namespace CodeBase.UI.ContentFiller
{
  public class ParentProvider : MonoBehaviour
  {
    [SerializeField] private GameObject _parent;

    public GameObject ParentObject()
    {
      return _parent;
    }
  }
}