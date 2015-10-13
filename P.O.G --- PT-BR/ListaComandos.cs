using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POG_BR
{
    class ListaComandos
    {
        private ComandoNo primeiro, ultimo;
        private int tamanho;

        public ListaComandos()
        {
            primeiro = null;
            ultimo = null;
            tamanho = 0;
        }

        public Boolean estaVazia()
        {
            if (tamanho == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void inserir(ComandoNo no)
        {
            if (estaVazia()) {
                no.setAnterior(no);
                no.setPosterior(no);
                primeiro = no;
                ultimo = no;
                tamanho++;
            } else {
                ultimo.setPosterior(no);
                no.setAnterior(ultimo);
                ultimo = no;
                tamanho++;
            }
        }

        public void inserirOrdenado(ComandoNo no)
        {
            if (estaVazia())
            {
                inserir(no);
            }
            else
            {
                ComandoNo anterior = null;
                ComandoNo atual = primeiro;
                for (int ct = 0; ct < tamanho; ct++)
                {
                    if (atual.getComando().CompareTo(no.getComando()) > 0) {
                        if (anterior == null)
                        {
                            no.setPosterior(atual);
                            primeiro = no;
                            tamanho++;
                            break;
                        }
                        else
                        {
                            anterior.setPosterior(no);
                            no.setAnterior(anterior);
                            no.setPosterior(atual);
                            atual.setAnterior(no);
                            tamanho++;
                            break;
                        }
                    }
                    else if (atual.getComando().CompareTo(no.getComando()) == 0)
                    {
                        throw new Exception("COMANDO JA EXISTENTE!");
                    }
                    else if (atual == ultimo) {
                        inserir(no);
                    }
                    else
                    {
                        anterior = atual;
                        atual = atual.getPosterior();
                    }
                }
            }
        }

        public ComandoNo pesquisaComando(String comando)
        {
            ComandoNo atual = primeiro;
            for (int ct = 0; ct < tamanho; ct++)
            {
                if (atual.getComando().Equals(comando))
                {
                    return atual;
                }
                else
                {
                    atual = atual.getPosterior();
                }
            }
            return null;
        }

        public void removerNo(String comando) {
            ComandoNo no = pesquisaComando(comando);
            no.getAnterior().setPosterior(no.getPosterior());
            no.getPosterior().setAnterior(no.getAnterior());
            tamanho--;
        }

        public int getTamanho()
        {
            return tamanho;
        }

        public ComandoNo getPrimeiro()
        {
            return primeiro;
        }

        public ComandoNo getUltimo()
        {
            return ultimo;
        }
    }
}
