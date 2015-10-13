using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POG_BR
{
    class ComandoNo
    {
        private String comando,operacaoA,operacaoF;
        private ComandoNo anterior, posterior;
        private int type;

        public ComandoNo(String comando,String operacaoA,String operacaoF,int type)
        {
            if (!comando.Equals("") && !operacaoA.Equals("") && !operacaoF.Equals("") && !(type < 0) && !(type>1))
            {
                this.comando = comando;
                this.operacaoA = operacaoA;
                this.operacaoF = operacaoF;
                this.type = type;
                this.anterior = null;
                this.posterior = null;
            }
            else
            {
                throw new Exception("PARAMETROS INVALIDOS");
            }
        }

        public void setAnterior(ComandoNo no)
        {
            if(no != null){
                this.anterior = no;
            }else{
                throw new Exception("PARAMETROS INVALIDOS");
            }
        }

        public void setPosterior(ComandoNo no)
        {
            if (no != null)
            {
                this.posterior = no;
            }
            else
            {
                throw new Exception("PARAMETROS INVALIDOS");
            }
        }

        public ComandoNo getAnterior()
        {
            return anterior;
        }

        public ComandoNo getPosterior()
        {
            return posterior;
        }


        public String getComando()
        {
            return comando;
        }

        public String getOperacaoA()
        {
            return operacaoA;
        }

        public String getOperacaoF()
        {
            return operacaoF;
        }

        public int getType()
        {
            return type;
        }

        public String toString()
        {
            return comando + "@" + operacaoA + "@" + operacaoF;
        }
    }
}
