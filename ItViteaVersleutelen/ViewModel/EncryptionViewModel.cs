using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using ItViteaVersleutelen.Model;

namespace ItViteaVersleutelen.ViewModel
{
    class EncryptionViewModel
    {
        public EncryptionViewModel()
        {
            EncryptionObj = new EncryptionClass();
        }
        public EncryptionClass EncryptionObj { get; set; }

        public void Encrypt()
        {
            EncryptionObj.EncryptText();
        }

        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        private class Updater : ICommand
        {
            #region ICommand Members  

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
            }

            #endregion
        }
    }
}
