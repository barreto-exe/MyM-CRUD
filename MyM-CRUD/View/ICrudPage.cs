using System;
using System.Collections.Generic;
using System.Text;

namespace MyM_CRUD.View
{
    public interface ICrudPage
    {
        public State CurrentState { get; set; }

        public enum State
        {
            Creating,
            Reading,
            Updating,
        }

        void SetCreating();
        void SetReading();
        void SetUpdating();
    }
}
