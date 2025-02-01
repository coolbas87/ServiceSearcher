using System.Windows.Input;

namespace ServiceSearcher
{
    internal abstract class ManagedCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public CommandManager? CommandManager { get; set; } = null;

        public abstract bool CanExecute(object? parameter);
        public abstract void Execute(object? parameter);

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    internal class CommandManager
    {
        private List<ManagedCommand> commands = new List<ManagedCommand>();

        public void AddCommand(ManagedCommand command)
        {
            if (!commands.Contains(command))
            {
                commands.Add(command);
                command.CommandManager = this;
            }
        }

        public void DeleteCommand(ManagedCommand command)
        {
            if (commands.Contains(command))
            {
                commands.Remove(command);
                command.CommandManager = null;
            }
        }

        public void Clear()
        {
            foreach (var command in commands)
            {
                command.CommandManager = null;
            }

            commands.Clear();
        }

        public void RaiseCanExecuteChanged()
        {
            foreach (var command in commands)
            {
                command.RaiseCanExecuteChanged();
            }
        }
    }
}
