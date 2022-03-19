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
    public static class CommandHistory
    {
        private static QueueStack<IAction> queueStackNormal { get; set; }
        private static QueueStack<IAction> queueStackReverse { get; set; }

        static CommandHistory()
        {
            queueStackNormal = new QueueStack<IAction>();
            queueStackReverse = new QueueStack<IAction>();
        }

        public static void Executed(IAction newAction)
        {
            queueStackNormal.Push(newAction);
            ClearReverse();
        }

        public static void Undo()
        {
            var UndoAction = queueStackNormal.Pop();
            if (UndoAction != null)
            {
                UndoAction.Undo();
                queueStackReverse.Push(UndoAction);
            }
        }

        public static void Redo()
        {
            var RedoAction = queueStackReverse.Pop();
            if (RedoAction != null)
            {
                RedoAction.Redo();
                queueStackNormal.Push(RedoAction);
            }
        }

        public static void ClearNormal()
        {
            queueStackNormal.Clear();
        }

        public static void ClearReverse()
        {
            if (queueStackNormal.HasItems)
            {
                queueStackReverse.Clear();
            }
        }
    }
}
