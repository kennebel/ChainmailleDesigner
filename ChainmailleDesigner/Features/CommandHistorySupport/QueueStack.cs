using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainmailleDesigner.Features.CommandHistorySupport
{
    public class QueueStack<T>
    {
        #region Fields
        private LinkedList<T> dataCollection { get; set; }
        public bool HasItems {  get {  return dataCollection.Count > 0; } }
        #endregion

        #region Construct / Destruct
        public QueueStack()
        {
            dataCollection = new LinkedList<T>();
        }
        #endregion

        #region Public Methods
        public void Push(T item)
        {
            dataCollection.AddFirst(item);
        }

        public T Pop()
        {
            var Top = dataCollection.FirstOrDefault();
            if (dataCollection.Count > 0) { dataCollection.RemoveFirst(); }
            return Top;
        }

        public void Clear()
        {
            dataCollection.Clear();
        }
        #endregion
    }
}
