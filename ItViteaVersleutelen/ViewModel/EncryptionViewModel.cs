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
            EncrypionClassObj = new EncryptionClass();
        }

        public EncryptionClass EncrypionClassObj { get; set; }
    }
}
