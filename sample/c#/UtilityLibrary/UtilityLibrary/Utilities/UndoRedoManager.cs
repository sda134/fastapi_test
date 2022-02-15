using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;

namespace UtilityLibrary.Utilities
{
    // http://anis774.net/codevault/undoredomanager.html 参照元

    public class UndoRedoManager_Instance<T> : UndoRedoManager_Base<T>
    {
        public T Instance
        { get => base._instance; set => base._instance = value; }
    }


    public class UndoRedoManager_Action : UndoRedoManager_Base<ActionCommand>
    {
        /*
         * 
         * 使い方
         * 
         * 
                var processManager = new UndoRedoManager_Action();

                processManager.Do(new ActionCommand(
                () =>
                {
                    // Do
                },
                () =>
                {
                    // Undo
                }));
         */
        public override void Do(ActionCommand cmd)
        {
            base.Do(cmd);
            cmd.Execute();
        }
        

        public void Do(Action exec, Action unExec)
        {
            this.Do(new ActionCommand(exec, unExec));
        }


        // Push Method : 18.02.22 [ver: 1.0.5.1] 追加
        /// <summary>
        /// 引数の Action を Excute する事なく Action の Push のみします
        /// </summary>
        public void Push(ActionCommand cmd)
        {
            base.Do(cmd);
        }

        // Push Method : 18.02.22 [ver: 1.0.5.1] 追加
        /// <summary>
        /// 引数の Action を Excute する事なく Action の Push のみします
        /// </summary>
        public void Push(Action exec, Action unExec)
        {
            this.Push(new ActionCommand(exec, unExec));
        }


        public override void Undo()
        {
            bool canUndo = base.CanUndo;

            base.Undo();

            // 18.02.22_1 修正 [ver: 1.0.5.1] 
            if (canUndo) base._instance.Unexecute();
        }

        public override void Redo()
        {
            bool canRedo = base.CanRedo;

            base.Redo();

            // 18.02.22_1 修正 [ver: 1.0.5.1] 
            if (canRedo) base._instance.Execute();
        }
    }


    public class ActionCommand
    {
        Action _exec = () => { };
        Action _unExec = () => { };

        public ActionCommand(Action exec, Action unExec)
        {
            if (exec != null) _exec = exec;
            if (unExec != null) _unExec = unExec;
        }

        public void Execute()
        {
            _exec.Invoke();
        }
        public void Unexecute()
        {
            _unExec.Invoke();
        }
    }



    class UndoRedoChangedEventArgs : EventArgs
    {
        public int Count;
    }


    /// <summary>
    /// ライブラリの UndoRedoManager をもう少し使いやすくする為に実験的に単独クラスを作る
    /// </summary>
    public class UndoRedoManager_Base<T>
    {
        Stack<T> _undo = new Stack<T>();
        Stack<T> _redo = new Stack<T>();

        public event EventHandler CanUndoChange = (obj, e) => { };
        public event EventHandler CanRedoChange = (obj, e) => { };

        public int UndoCount { get => _undo.Count; }
        public int RedoCount { get => _redo.Count; }


        protected T _instance;

        public virtual void Do(T instance)
        {
            _redo.Clear();

            // 上からデータを入れる
            _undo.Push(instance);

            // イベントを発生させる
            this.CanUndoChange.Invoke(this, new UndoRedoChangedEventArgs { Count = this._undo.Count});
            this.CanRedoChange.Invoke(this, new UndoRedoChangedEventArgs { Count = this._redo.Count });
        }


        public virtual void Undo()
        {
            if (!this.CanUndo) return;


            // 上からデータを取り出す
            this._instance = _undo.Pop();

            // 上からデータを入れる
            _redo.Push(this._instance);

            // イベントを発生させる
            this.CanUndoChange.Invoke(this, new UndoRedoChangedEventArgs { Count = this._undo.Count });
            this.CanRedoChange.Invoke(this, new UndoRedoChangedEventArgs { Count = this._redo.Count });
        }

        public virtual void Redo()
        {
            if (!this.CanRedo) return;

            // 上からデータを取り出す
            this._instance = _redo.Pop();

            // 上からデータを入れる
            _undo.Push(this._instance);

            // イベントを発生させる
            this.CanUndoChange.Invoke(this, new UndoRedoChangedEventArgs { Count = this._undo.Count });
            this.CanRedoChange.Invoke(this, new UndoRedoChangedEventArgs { Count = this._redo.Count });
        }

        public void ClearHistory()
        {
            _undo.Clear();
            _redo.Clear();

            // イベントを発生させる
            this.CanUndoChange.Invoke(this, new UndoRedoChangedEventArgs { Count = this._undo.Count });
            this.CanRedoChange.Invoke(this, new UndoRedoChangedEventArgs { Count = this._redo.Count });
        }

        public bool CanUndo { get { return _undo.Count > 0; } }
        public bool CanRedo { get { return _redo.Count > 0; } }
    }
}