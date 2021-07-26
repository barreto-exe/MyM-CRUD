using MyM_CRUD.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MyM_CRUD.View
{
    public interface ICrudPage<T>
    {
        public State CurrentState { get; set; }

        public enum State
        {
            Creating,
            Reading,
            Updating,
        }

        public void TxtSearch_TextChanged(object sender, TextChangedEventArgs e);
        public void Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e);
        public void BtnEditSave_Click(object sender, RoutedEventArgs e)
        {
            switch (CurrentState)
            {
                //Click en editar
                case State.Reading:
                    {
                        BeginUpdating();
                        break;
                    }

                //Click en guardar
                case State.Creating:
                case State.Updating:
                    {
                        try
                        {
                            dynamic tuple = GetObjectsFromFields();

                            if (CurrentState == State.Creating)
                            {
                                tuple.InsertTupleDatabase();
                            }
                            if (CurrentState == State.Updating)
                            {
                                tuple.UpdateTupleDataBase();
                            }
                        }
                        catch (Exception ex) { }

                        SetReading();
                        TxtSearch_TextChanged(null, null);
                        break;
                    }
            }
        }
        public void BtnAdd_Click(object sender, RoutedEventArgs e);

        public void LoadFields(T selected);
        public T GetObjectsFromFields();

        public void SetCreating();
        public void SetReading();
        public void SetUpdating();
        public void BeginUpdating();
    }
}
