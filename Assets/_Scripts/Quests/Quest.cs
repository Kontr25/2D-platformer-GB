using System;
using _Scripts.Character;

namespace _Scripts.Quests
{
    public class Quest : IQuest, IDisposable
    {
        private readonly QuestObjectView _view;
        private readonly IQuestModel _model;

        private bool _active;
        
        public event Action<IQuest> Completed;
        public bool IsCompleted { get; private set; }

        public Quest(QuestObjectView view, IQuestModel model)
        {
            _view = view;
            _model = model;
        }

        private void Complete()
        {
            if(!_active)
                return;

            _active = false;
            IsCompleted = true;
            _view.OnLevelObjectContact -= OnContact;
            _view.ProcessComplete();
            OnCompleted();
        }

        private void OnCompleted()
        {
            Completed?.Invoke(this);
        }

        public void Reset()
        {
            if(_active)
                return;

            _active = true;
            IsCompleted = false;
            _view.OnLevelObjectContact += OnContact;
            _view.ProcessActivate();
        }

        private void OnContact(CharacterView characterView)
        {
            var completed = _model.TryComplete(characterView.gameObject);

            if (completed)
                Complete();
        }
        
        public void Dispose()
        {
            _view.OnLevelObjectContact -= OnContact;
        }
    }
}