﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class FormManager
    {
        private static int _currentForm; /* current type of form */

        ///<summary>
        ///@funtion currentForm: The current type of form
        /// </summary>
        public static int currentForm
        {
            get
            {
                return _currentForm;
            }
        }

        ///<summary>
        ///@funtion openForm: Open the forms in the application
        ///<para></para>
        ///@param formType: The type of form to open
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        public static void openForm(int formType, SuperEventArgs e = null)
        {
            switch (formType)
            {
                case Constants.FORM_ACCOUNT:
                    _currentForm = Constants.FORM_ACCOUNT;
                    FormAccount formAccount = new FormAccount();
                    formAccount.ShowDialog();
                    break;
                case Constants.FORM_PLAY:
                    string opponentName, clientName;
                    FormPlay formPlay;
                    _currentForm = Constants.FORM_PLAY;
                    opponentName = FormMain.App.opponentName;
                    clientName = FormMain.App.getPlayerName();
                    bool isClientGoFirst = e.ReturnText == "" ? false : true;
                    formPlay = new FormPlay(opponentName, clientName, isClientGoFirst);
                    formPlay.ShowDialog();
                    break;
                case Constants.FORM_MAIN:
                    _currentForm = Constants.FORM_MAIN;
                    break;
                default:
                    break;
            }
        }


    }
}
