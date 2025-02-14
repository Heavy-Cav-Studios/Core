using System;
using System.Collections.Generic;
using UnityEngine;

namespace HeavyCavStudios.Core.Collections
{
    [Serializable]
    public class ObservableList<T>
    {
        public Action<T> OnElementAdded;
        public Action<T> OnElementRemoved;
        
        [SerializeField]
        List<T> m_List;
        
        public ObservableList()
        {
            m_List = new List<T>();
        }
        
        public ObservableList(IEnumerable<T> list)
        {
            m_List = new List<T>(list);
        }

        public void Add(T element)
        {
            m_List.Add(element);
            OnElementAdded?.Invoke(element);
        }

        public void Insert(int index, T element)
        {
            m_List.Insert(index, element);
            OnElementAdded?.Invoke(element);
        }

        public bool Remove(T element)
        {
            var removeSuccess = m_List.Remove(element);

            if (removeSuccess)
            {
                OnElementRemoved.Invoke(element);
            }

            return removeSuccess;
        }

        public void RemoveAt(int index)
        {
            var removedElement = m_List[index];
            m_List.RemoveAt(index);
            OnElementRemoved?.Invoke(removedElement);
        }
        public int Count => m_List.Count;

        public T this[int index]
        {
            get => m_List[index];
            set
            {
                var originalValue = m_List[index];
                m_List[index] = value;
                OnElementAdded?.Invoke(value);
                OnElementRemoved?.Invoke(originalValue);
            }
        }

        public List<T> ToList() => new List<T>(m_List);

        public IEnumerator<T> GetEnumerator() => m_List.GetEnumerator();
        
    }
}
