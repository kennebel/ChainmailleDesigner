using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainmailleDesigner.Features.CommandHistorySupport
{
    public class QueueStack<T>
    {
        #region Attributes
        private LinkedList<T> dataCollection { get; set; }
        #endregion

        #region COnstruct / Destruct
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
            if (dataCollection.Count() > 0)
            {
                var Top = dataCollection.First();
                dataCollection.RemoveFirst();
                return Top;
            }
            else
            {
                return default;
            }
        }

        public void Clear()
        {
            dataCollection.Clear();
        }
        #endregion
    }
}
