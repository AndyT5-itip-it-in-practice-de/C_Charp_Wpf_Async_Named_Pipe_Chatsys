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

    /*
     * //serializes ------------------------------------------------------------------------------------------------------------------
           
                    Message_Translator MessTransl = new Message_Translator
                    {
                        MessageCategory = txtbx_Message_Category.Text,
                        Server_ActionNeedet = txtbx_Message_Server_ActionNeedet.Text,
                        //bool mit großem true?????? soll klein
                        Server_IP = txtbx_Message_Server_IP.Text,
                        Server_PipeName = txtbx_Message_Server_PipeName.Text,

                        value1 = txtbx_Message_Value1.Text,
                        value2 = txtbx_Message_Value2.Text,
                        value3 = txtbx_Message_Value3.Text,
                        value4 = txtbx_Message_Value4.Text,
                        value5 = txtbx_Message_Value5.Text,

                        Client_ActionNeedet = txtbx_Message_Client_ActionNeedet.Text,
                        Client_AnswerNeedet = txtbx_Message_Client_AnswerNeedet.Text,
                    };

                    string jsonString = JsonConvert.SerializeObject(MessTransl, Formatting.Indented);

                    txtbx_Message_Created_jsonString.Text = jsonString;

     * 
     * 
     *  //Deserializes -----------------------------------------------------------------------------------------------------------------------

            string jsonString = txtbx_Message_Created_jsonString_input_fromPipe_Decode.Text;  

            Message_Translator MessTransl = JsonConvert.DeserializeObject<Message_Translator>(jsonString);
            { 
                txtbx_catch_Message_Category.Text = (MessTransl.MessageCategory);
                txtbx_catch_Message_Server_ActionNeedet.Text = (MessTransl.Server_ActionNeedet).ToString();

                txtbx_catch_Message_Server_IP.Text = (MessTransl.Server_IP);
                txtbx_catch_Message_Server_PipeName.Text = (MessTransl.Server_PipeName);

                txtbx_catch_Message_Value1.Text = (MessTransl.value1);
                txtbx_catch_Message_Value2.Text = (MessTransl.value2);
                txtbx_catch_Message_Value3.Text = (MessTransl.value3);
                txtbx_catch_Message_Value4.Text = (MessTransl.value4);
                txtbx_catch_Message_Value5.Text = (MessTransl.value5);

                txtbx_catch_Message_Client_ActionNeedet.Text = (MessTransl.Client_ActionNeedet).ToString();
                txtbx_catch_Message_Client_AnswerNeedet.Text = (MessTransl.Client_AnswerNeedet).ToString();
            }
     * 
     * 
     * 
     * 
     */
}
