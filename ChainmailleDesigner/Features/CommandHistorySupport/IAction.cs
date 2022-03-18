using System;

namespace ChainmailleDesigner.Features.CommandHistorySupport
{
    public interface IAction
    {

        void execute();

        void undo();

        string getName();
    }
}
