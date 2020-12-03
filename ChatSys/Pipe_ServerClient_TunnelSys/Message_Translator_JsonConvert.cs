using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSys.MessageServer
{
    class Message_Translator
    {
        public string MessageCategory { get; set; }     // LogWrite  // AdjustWrite // AdjustProgr // ActStarter // InfoMsg  // 
        public string Server_ActionNeedet { get; set; }          // true  //  false

        public string Server_IP { get; set; }
        public string Server_PipeName { get; set; }

        public string value1 { get; set; }
        public string value2 { get; set; }
        public string value3 { get; set; }
        public string value4 { get; set; }
        public string value5 { get; set; }

        public string Client_ActionNeedet { get; set; }          // true  //  false
        public string Client_AnswerNeedet { get; set; }          // true  //  false
    }
}
