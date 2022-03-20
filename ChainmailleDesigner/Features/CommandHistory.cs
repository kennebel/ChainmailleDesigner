using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainmailleDesigner.Features.CommandHistorySupport;

namespace ChainmailleDesigner.Features
{
    /// <summary>
    /// Architecture inspired by an article from Nasi Jofche
    /// https://betterprogramming.pub/utilizing-the-command-pattern-to-support-undo-redo-and-history-of-operations-b28fa9d58910
    /// 
    /// Undo/Redo system for Chainmaille Designer
    /// </summary>
    public class CommandHistory
    {
        #region Event Handling
        public delegate void HistoryChangedEventHandler(object source, EventArgs args);
        public event HistoryChangedEventHandler HistoryChanged;

        public class HistoryStatus : EventArgs
        {
            public bool HasUndoAvailable { get; set; }
            public bool HasRedoAvailable { get; set; }
        }
        #endregion

        #region Fields
        public static CommandHistory instance { get; private set; }

        private QueueStack<IAction> queueStackNormal { get; set; }
        private QueueStack<IAction> queueStackReverse { get; set; }

        public static bool HasUndoAvailable { get { return instance.queueStackNormal.HasItems; } }
        public static bool HasRedoAvailable { get { return instance.queueStackReverse.HasItems; } }
        #endregion

        #region Construct / Destruct
        static CommandHistory()
        {
            instance = new CommandHistory();
        }

        private CommandHistory()
        {
            queueStackNormal = new QueueStack<IAction>();
            queueStackReverse = new QueueStack<IAction>();
        }
        #endregion

        #region Methods
        public static void Executed(IAction newAction)
        {
            instance.queueStackNormal.Push(newAction, limitQueue:true);
            instance.ClearReverse();
        }

        public static void Executed(List<IAction> newActionGroup)
        {
            // TODO: Add support for a group of actions simultaneously
            //instance.queueStackNormal.Push(newAction);
            instance.ClearReverse();
        }

        public static void Undo()
        {
            var UndoAction = instance.queueStackNormal.Pop();
            if (UndoAction != null)
            {
                UndoAction.Undo();
                instance.queueStackReverse.Push(UndoAction);
                instance.TriggerEvent();
            }
        }

        public static void Redo()
        {
            var RedoAction = instance.queueStackReverse.Pop();
            if (RedoAction != null)
            {
                RedoAction.Redo();
                instance.queueStackNormal.Push(RedoAction);
                instance.TriggerEvent();
            }
        }

        private void TriggerEvent()
        {
            if (HistoryChanged != null)
            {
                var HistoryEventArgs = new HistoryStatus() { HasUndoAvailable = HasUndoAvailable, HasRedoAvailable = HasRedoAvailable };
                HistoryChanged(this, HistoryEventArgs);
            }
        }

        private void ClearReverse()
        {
            if (queueStackNormal.HasItems)
            {
                queueStackReverse.Clear();
                TriggerEvent();
            }
        }
        #endregion
    }
}
