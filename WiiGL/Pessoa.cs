using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiGL
{
    public struct pessoa_reduzido
    {
        public int ID;
        public string nome;
        public string arquivo_completo;
        public List<string> capturas;
        public string EmString()
        {
            string temp = String.Format("{0}-{1}={2}", ID, nome, arquivo_completo);
            return temp;
        }
    }

    class Pessoa
    {
        public string get(int indice)
        {
            switch (indice)
            {
                //int
                case 0: return ID.ToString();
                case 1: return Cor.ToString();
                case 2: return Estado_civil.ToString();
                case 3: return Escolaridade.ToString();
                case 4: return Medicamentos.ToString();
                case 5: return Avaliação_saude.ToString();
                case 6: return Avaliação_visao.ToString();
                case 7: return Avaliação_audicao.ToString();
                case 8: return Grau_Dor.ToString();
                case 9: return Locomocao.ToString();
                case 10: return Transporte.ToString();
                case 11: return CF_pontos.ToString();
                case 12: return UQ_Outras.ToString();
                case 13: return ST_Tipo.ToString();
                case 14: return ST_EVA.ToString();
                case 15: return ST_Periodicidade.ToString();
                case 16: return GDS_pontos.ToString();
                case 17: return DGI_Pontos.ToString();
                //bool
                case 18: return Genero.ToString();
                case 19: return Hospitazacao_UA.ToString();
                case 20: return Cirurgia_UA.ToString();
                case 21: return Historico_Alcolismo.ToString();
                case 22: return Historico_Tabagismo.ToString();
                case 23: return Atividade_Fisica.ToString();
                case 24: return Sente_Dor.ToString();
                case 25: return Quedas.ToString();
                case 26: return Medo_de_Cair.ToString();
                case 27: return UQ_Ambiente.ToString();
                case 28: return UQ_Ajuda.ToString();
                case 29: return UQ_Lesao.ToString();
                case 30: return ST_Existe.ToString();
                case 31: return apresenta_defict.ToString();
                //string
                case 32: return Nome;
                case 33: return Profissão;
                case 34: return Endereço;
                case 35: return Numero_Casa;
                case 36: return Bairro;
                case 37: return Cidade;
                case 38: return Numero_Telefone;
                case 39: return Numero_Celular;
                case 40: return Numero_protocolo;
                case 41: return Medico_Responsável;
                case 42: return Avaliador;
                case 43: return Diagnostico_funcional;
                case 44: return Pontos_Importantes;
                case 45: return Arranjo_Moradia;
                case 46: return Hipoteses_diagnosticas;
                case 47: return Medicamentos_disc;
                case 48: return Queixa_Principal;
                case 49: return Dispositivo_Auxilio;
                case 50: return UQ_Mecanismo;
                case 51: return UQ_Circunstancia;
                //Datetime
                case 52: return Data_Coleta.ToString();
                case 53: return Nascimento.ToString();
                //float
                case 54: return TUGT_Tempo.ToString();
                //bool []
                case 55: return CF_tabela.ToString();
                case 56: return ST_Fatores.ToString();
                case 57: return DPC_EDGA.ToString();
                //int []
                case 58: return DPC_MEEM.ToString();
                case 59: return BBS_Tabela.ToString();
                case 60: return CTSIB_Tabela.ToString();
                case 61: return DGI_Tabela.ToString();
                default: return "Falha";
            }
        }
        public string get(int indice_var, int posição)
        {
            switch (indice_var)
            {
                //bool []
                case 55: return CF_tabela[posição].ToString();
                case 56: return ST_Fatores[posição].ToString();
                case 57: return DPC_EDGA[posição].ToString();
                //int []
                case 58: return DPC_MEEM[posição].ToString();
                case 59: return BBS_Tabela[posição].ToString();
                case 60: return CTSIB_Tabela[posição].ToString();
                case 61: return DGI_Tabela[posição].ToString();
                default: return "Falha";
            }
        }
        public int ID;
        public Pessoa()
        {
            for (int i = 0; i < 14; i++) CF_tabela[i] = false;
        }
        private string temp;
        public string Nome;
        public string Profissão;
        public DateTime Nascimento;
        public bool Genero;
        public int Cor;
        public String Endereço;
        public string Numero_Casa;
        public String Bairro;
        public String Cidade;
        public String Numero_Telefone;
        public String Numero_Celular;
        public String Numero_protocolo;
        public String Medico_Responsável;
        public String Avaliador;
        public DateTime Data_Coleta;
        public int Estado_civil;
        public String Diagnostico_funcional;
        public String Pontos_Importantes;
        public int Escolaridade;
        public String Arranjo_Moradia;
        public String Hipoteses_diagnosticas;
        public int Medicamentos;
        public string Medicamentos_disc;
        public String Queixa_Principal;
        public bool Hospitazacao_UA;
        public bool Cirurgia_UA;
        public bool Historico_Alcolismo;
        public bool Historico_Tabagismo;
        public bool Atividade_Fisica;
        public int Avaliação_saude;
        public int Avaliação_visao;
        public int Avaliação_audicao;
        public bool Sente_Dor;
        public int Grau_Dor;
        public int Locomocao;
        public int Transporte;
        public string Dispositivo_Auxilio;
        public bool Quedas;
        public bool Medo_de_Cair;
        //Tabela de capacitação funcional
        public bool[] CF_tabela = new bool[15];
        public int CF_pontos;
        public string CF_avaliar()
        {
            CF_pontos = 0;
            for (int i = 0; i < 15; i++) if (CF_tabela[i]) CF_pontos++;
            if (CF_pontos == 0) temp = "Sem Comprometimento";
            else if (CF_pontos < 4) temp = "Comprometimento leve";
            else if (CF_pontos < 7) temp = "Comprometimento moderado";
            else temp = "Comprometimento grave";
            return temp;
        }
        //Última queda
        public bool UQ_Ambiente;
        public bool UQ_Ajuda;
        public bool UQ_Lesao;
        public string UQ_Mecanismo;
        public string UQ_Circunstancia;
        public int UQ_Outras;
        //Sobre tontura
        public bool ST_Existe;
        public int ST_Tipo;
        public int ST_EVA;
        public int ST_Periodicidade;
        public bool[] ST_Fatores = new bool[12];
        //Dados psico_cognitivos
        //MEEM
        public int[] DPC_MEEM = new int[10];
        public int MEEN_pontua()
        {
            int pontos = 0;
            for (int i = 0; i < 10; i++) pontos += DPC_MEEM[i];
            return pontos;
        }
        public bool apresenta_defict;
        //EDGA
        public bool[] DPC_EDGA = new bool[30];
        public int GDS_pontos;
        public string GDS_categoria()
        {
            GDS_pontos = 0;
            for (int i = 0; i < 30; i++) if (DPC_EDGA[i]) GDS_pontos++;
            if (GDS_pontos < 5) temp = "Normal";
            else if (GDS_pontos < 11) temp = "Depressão Leve";
            else temp = "Depressão Grave";
            return temp;
        }
        //TUGT
        public float TUGT_Tempo;
        public string TUGT_Risco()
        {
            temp = (TUGT_Tempo < 13.5 ? "Menor" : "Maior") + " risco de queda";
            return temp;
        }
        //BBS
        public int[] BBS_Tabela = new int[14];
        public int BBS_Pontos()
        {
            int pontos = 0;
            for (int i = 0; i < 14; i++) pontos += BBS_Tabela[i];
            return pontos;
        }
        public string BBS_Categoria()
        {
            temp = (BBS_Pontos() < 46 ? "Maior" : "Menor") + " risco de queda";
            return temp;
        }
        //CTSIB
        public int[] CTSIB_Tabela = new int[4];
        public string CTSIB_Classifica(int t)
        {
            if (t >= 30) return "Normal";
            else return "Alterado";
        }
        //DGI
        public int[] DGI_Tabela = new int[8];
        public int DGI_Pontos;
        public string DGI_Categoria()
        {
            DGI_Pontos = 0;
            for (int i = 0; i < 8; i++) DGI_Pontos += DGI_Tabela[i];
            return (DGI_Pontos > 19 ? "Menor" : "Maior") + " risco de queda.";
        }
        public pessoa_reduzido resumo()
        {
            pessoa_reduzido temp;
            temp.nome = Nome;
            temp.ID = ID;
            temp.capturas = new List<string>();
            temp.arquivo_completo = "../Dados/" + ID.ToString() + "_" + Nome + ".txt";
            return temp;
        }
        public void Limpar()
        {
            int i;
            for (i = 0; i < 14; i++) CF_tabela[i] = false;
            Nome = " "; Profissão = " "; Nascimento = System.DateTime.Now;
            Genero = true; Cor = -1; Endereço = " "; Numero_Casa = " ";
            Bairro = " "; Cidade = " "; Numero_Telefone = " ";
            Numero_Celular = " "; Numero_protocolo = " "; Medico_Responsável = " ";
            Avaliador = " "; Data_Coleta = System.DateTime.Now;
            Estado_civil = -1; Diagnostico_funcional = " "; Pontos_Importantes = " ";
            Escolaridade = -1; Arranjo_Moradia = " "; Hipoteses_diagnosticas = " ";
            Medicamentos = 0; Medicamentos_disc = " "; Queixa_Principal = " ";
            Hospitazacao_UA = false; Cirurgia_UA = false; Historico_Alcolismo = false;
            Historico_Tabagismo = false; Atividade_Fisica = false;
            Avaliação_saude = 0; Avaliação_visao = 0; Avaliação_audicao = 0;
            Sente_Dor = false; Grau_Dor = 0; Locomocao = -1; Transporte = -1;
            Dispositivo_Auxilio = " "; Quedas = false; Medo_de_Cair = false;
            UQ_Ambiente = false; UQ_Ajuda = false; UQ_Lesao = false; UQ_Mecanismo = " ";
            UQ_Circunstancia = " "; UQ_Outras = 0;
            ST_Existe = false; ST_Tipo = 0; ST_EVA = 0; ST_Periodicidade = 0;
            for (i = 0; i < 12; i++) ST_Fatores[i] = false;
            for (i = 0; i < 10; i++) DPC_MEEM[i] = 0;
            apresenta_defict = false;
            for (i = 0; i < 30; i++) DPC_EDGA[i] = false;
            GDS_pontos = 0; TUGT_Tempo = 0;
            for (i = 0; i < 14; i++) BBS_Tabela[i] = 0;
            for (i = 0; i < 4; i++) CTSIB_Tabela[i] = 0;
            for (i = 0; i < 8; i++) DGI_Tabela[i] = 0;
            DGI_Pontos = 0;
        }
        public void copiar(Pessoa a)
        {
            int i;
            for (i = 0; i < 14; i++) CF_tabela[i] = a.CF_tabela[i];
            Nome = a.Nome; Profissão = a.Profissão; Nascimento = a.Nascimento;
            Genero = a.Genero; Cor = a.Cor; Endereço = a.Endereço; Numero_Casa = a.Numero_Casa;
            Bairro = a.Bairro; Cidade = a.Cidade; Numero_Telefone = a.Numero_Telefone;
            Numero_Celular = a.Numero_Celular; Numero_protocolo = a.Numero_protocolo; Medico_Responsável = a.Medico_Responsável;
            Avaliador = a.Avaliador; Data_Coleta = a.Data_Coleta;
            Estado_civil = a.Estado_civil; Diagnostico_funcional = a.Diagnostico_funcional; Pontos_Importantes = a.Pontos_Importantes;
            Escolaridade = a.Escolaridade; Arranjo_Moradia = a.Arranjo_Moradia; Hipoteses_diagnosticas = a.Hipoteses_diagnosticas;
            Medicamentos = a.Medicamentos; Medicamentos_disc = a.Medicamentos_disc; Queixa_Principal = a.Queixa_Principal;
            Hospitazacao_UA = a.Hospitazacao_UA; Cirurgia_UA = a.Cirurgia_UA; Historico_Alcolismo = a.Historico_Alcolismo;
            Historico_Tabagismo = a.Historico_Tabagismo; Atividade_Fisica = a.Atividade_Fisica;
            Avaliação_saude = a.Avaliação_saude; Avaliação_visao = a.Avaliação_visao; Avaliação_audicao = a.Avaliação_audicao;
            Sente_Dor = a.Sente_Dor; Grau_Dor = a.Grau_Dor; Locomocao = a.Locomocao; Transporte = a.Transporte;
            Dispositivo_Auxilio = a.Dispositivo_Auxilio; Quedas = a.Quedas; Medo_de_Cair = a.Medo_de_Cair;
            UQ_Ambiente = a.UQ_Ambiente; UQ_Ajuda = a.UQ_Ajuda; UQ_Lesao = a.UQ_Lesao; UQ_Mecanismo = a.UQ_Mecanismo;
            UQ_Circunstancia = a.UQ_Circunstancia; UQ_Outras = a.UQ_Outras;
            ST_Existe = a.ST_Existe;
            if (ST_Existe)
            {
                ST_Tipo = a.ST_Tipo; ST_EVA = a.ST_EVA; ST_Periodicidade = a.ST_Periodicidade;
                for (i = 0; i < 12; i++) ST_Fatores[i] = a.ST_Fatores[i];
            }
            for (i = 0; i < 10; i++) DPC_MEEM[i] = a.DPC_MEEM[i];
            apresenta_defict = a.apresenta_defict;
            for (i = 0; i < 30; i++) DPC_EDGA[i] = a.DPC_EDGA[i];
            GDS_pontos = a.GDS_pontos; TUGT_Tempo = a.TUGT_Tempo;
            for (i = 0; i < 14; i++) BBS_Tabela[i] = a.BBS_Tabela[i];
            for (i = 0; i < 4; i++) CTSIB_Tabela[i] = a.CTSIB_Tabela[i];
            for (i = 0; i < 8; i++) DGI_Tabela[i] = a.DGI_Tabela[i];
            DGI_Pontos = a.DGI_Pontos;
        }

        public List<string> raca;
        public List<string> estadoC;
        public List<string> educacao;
        public List<string> avaliar;
        public List<string> movimento;
        public List<string> ST_Tipo_Tontura;
        public List<string> propriedades_pessoa;
        public List<int> tam_vetores;

        public void carregar_listas()
        {
            raca = new List<string>();
            estadoC = new List<string>();
            educacao = new List<string>();
            avaliar = new List<string>();
            movimento = new List<string>();
            ST_Tipo_Tontura = new List<string>();
            propriedades_pessoa = new List<string>();
            tam_vetores = new List<int>(new int[] { 15, 12, 30, 10, 14, 4, 8 });

            raca.Add("Raça");
            raca.Add("Branco(a)");
            raca.Add("Negro(a)");
            raca.Add("Pardo(a)");
            raca.Add("Amarelo(a)");
            raca.Add("Miscigenado(a)");
            raca.Add("Índio(a)");
            raca.Add("Asiático(a)");

            estadoC.Add("Solteiro(a)");
            estadoC.Add("Casado(a)");
            estadoC.Add("Divorciado(a)");
            estadoC.Add("Viúvo(a)");
            estadoC.Add("União estável");

            educacao.Add("Analfabeto(a)");
            educacao.Add("Básica Incompleta(Primário)");
            educacao.Add("Básica Completa(Primário)");
            educacao.Add("Fundamental Incompleta(1º grau, Ginásio)");
            educacao.Add("Fundamental Completa(1º grau, Ginásio)");
            educacao.Add("Médio Imcompleto(2º grau, Colegial)");
            educacao.Add("Médio Completo(2º grau, Colegial)");
            educacao.Add("Superior Imcompleto(3º grau)");
            educacao.Add("Superior Completo(3º grau)");
            educacao.Add("Mestrado Imcompleto");
            educacao.Add("Mestrado Completo");
            educacao.Add("Doutorado Imcompleto");
            educacao.Add("Doutorado Completo");

            avaliar.Add("Excelente");
            avaliar.Add("Muito Boa");
            avaliar.Add("Boa");
            avaliar.Add("Ruim");
            avaliar.Add("Muito Ruim");

            movimento.Add("Independente");
            movimento.Add("Com supervisão");
            movimento.Add("Auxílio Parcial");
            movimento.Add("Auxílio Total");

            ST_Tipo_Tontura.Add("Não rotatória");
            ST_Tipo_Tontura.Add("Vertigem Subjetiva");
            ST_Tipo_Tontura.Add("Vertigem Objetiva");

            //int
            propriedades_pessoa.Add("ID");//0
            propriedades_pessoa.Add("Cor");//1
            propriedades_pessoa.Add("Estado_civil");//2
            propriedades_pessoa.Add("Escolaridade");//3
            propriedades_pessoa.Add("Medicamentos");//4
            propriedades_pessoa.Add("Avaliação_saude");//5
            propriedades_pessoa.Add("Avaliação_visao");//6
            propriedades_pessoa.Add("Avaliação_audicao");//7
            propriedades_pessoa.Add("Grau_Dor");//8
            propriedades_pessoa.Add("Locomocao");//9
            propriedades_pessoa.Add("Transporte");//10
            propriedades_pessoa.Add("CF_pontos");//11
            propriedades_pessoa.Add("UQ_Outras");//12
            propriedades_pessoa.Add("ST_Tipo");//13
            propriedades_pessoa.Add("ST_EVA");//14
            propriedades_pessoa.Add("ST_Periodicidade");//15
            propriedades_pessoa.Add("GDS_pontos");//16
            propriedades_pessoa.Add("DGI_Pontos");//17
            //bool
            propriedades_pessoa.Add("Genero");//18
            propriedades_pessoa.Add("Hospitazacao_UA");//19
            propriedades_pessoa.Add("Cirurgia_UA");//20
            propriedades_pessoa.Add("Historico_Alcolismo");//21
            propriedades_pessoa.Add("Historico_Tabagismo");//22
            propriedades_pessoa.Add("Atividade_Fisica");//23
            propriedades_pessoa.Add("Sente_Dor");//24
            propriedades_pessoa.Add("Quedas");//25
            propriedades_pessoa.Add("Medo_de_Cair");//26
            propriedades_pessoa.Add("UQ_Ambiente");//27
            propriedades_pessoa.Add("UQ_Ajuda");//28
            propriedades_pessoa.Add("UQ_Lesao");//29
            propriedades_pessoa.Add("ST_Existe");//30
            propriedades_pessoa.Add("apresenta_defict");//31
            //string
            propriedades_pessoa.Add("Nome");//32
            propriedades_pessoa.Add("Profissão");//33
            propriedades_pessoa.Add("Endereço");//34
            propriedades_pessoa.Add("Numero_Casa");//35
            propriedades_pessoa.Add("Bairro");//36
            propriedades_pessoa.Add("Cidade");//37
            propriedades_pessoa.Add("Numero_Telefone");//38
            propriedades_pessoa.Add("Numero_Celular");//39
            propriedades_pessoa.Add("Numero_protocolo");//40
            propriedades_pessoa.Add("Medico_Responsável");//41
            propriedades_pessoa.Add("Avaliador");//42
            propriedades_pessoa.Add("Diagnostico_funcional");//43
            propriedades_pessoa.Add("Pontos_Importantes");//44
            propriedades_pessoa.Add("Arranjo_Moradia");//45
            propriedades_pessoa.Add("Hipoteses_diagnosticas");//46
            propriedades_pessoa.Add("Medicamentos_disc");//47
            propriedades_pessoa.Add("Queixa_Principal");//48
            propriedades_pessoa.Add("Dispositivo_Auxilio");//49
            propriedades_pessoa.Add("UQ_Mecanismo");//50
            propriedades_pessoa.Add("UQ_Circunstancia");//51
            //Datetime
            propriedades_pessoa.Add("Data_Coleta");//52
            propriedades_pessoa.Add("Nascimento");//53
            //float
            propriedades_pessoa.Add("TUGT_Tempo");//54
            //bool []
            propriedades_pessoa.Add("CF_tabela");//55
            propriedades_pessoa.Add("ST_Fatores");//56
            propriedades_pessoa.Add("DPC_EDGA");//67
            //int []
            propriedades_pessoa.Add("DPC_MEEM");//68
            propriedades_pessoa.Add("BBS_Tabela");//69
            propriedades_pessoa.Add("CTSIB_Tabela");//70
            propriedades_pessoa.Add("DGI_Tabela");//71

        }
    }
}
