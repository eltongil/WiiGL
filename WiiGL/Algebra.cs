using System;
using System.Collections.Generic;

namespace Algebra
{
    public class complexo
    {
        public double real, imaginario;
        public complexo(double Real, double Imaginario) { real = Real; imaginario = Imaginario; }
        public override string ToString()
        {
            if (imaginario != 0) return String.Format("{0} + {1}i", real, imaginario);
            else return String.Format("{0}", real);
        }
        public void copiar(complexo outro)
        {
            this.real = outro.real;
            this.imaginario = outro.imaginario;
        }
        public void copiar(double outro)
        {
            this.real = outro;
            this.imaginario = 0;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            complexo objCom = obj as complexo;
            if (objCom.real == this.real && objCom.imaginario == this.imaginario) return true;
            else return false;
        }
        public override int GetHashCode()
        {
            return (int)(imaginario + real);
        }
        public static complexo operator *(complexo a, complexo b)
        {
            return new complexo(a.real * b.real - a.imaginario * b.imaginario, a.real * b.imaginario + a.imaginario * b.real);
        }
        public static complexo operator *(complexo a, double b)
        {
            return new complexo(a.real * b, a.imaginario * b);
        }
        public static complexo operator *(double b, complexo a)
        {
            return new complexo(a.real * b, a.imaginario * b);
        }
        public static complexo operator +(complexo a, complexo b)
        {
            return new complexo(a.real + b.real, a.imaginario + b.imaginario);
        }
        public static complexo operator +(complexo a, double b)
        {
            return new complexo(a.real + b, a.imaginario);
        }
        public static complexo operator -(complexo a, complexo b)
        {
            return new complexo(a.real - b.real, a.imaginario - b.imaginario);
        }
        public static complexo operator -(complexo a, double b)
        {
            return new complexo(a.real - b, a.imaginario);
        }
        public static complexo operator /(complexo a, double b)
        {
            return new complexo(a.real / b, a.imaginario / b);
        }
        public static complexo operator /(complexo a, complexo b)
        {
            complexo temp = new complexo(0, 0);
            temp = (a * (~b)) / ((b * (~b)).real);
            return temp;
        }
        public static bool operator ==(complexo este, complexo outro)
        {
            if (este.real == outro.real && este.imaginario == outro.imaginario) return true;
            else return false;
        }
        public static bool operator !=(complexo este, complexo outro)
        {
            if (este.real == outro.real && este.imaginario == outro.imaginario) return false;
            else return true;
        }
        public static bool operator !=(complexo este, double outro)
        {
            if (este.real == outro && este.imaginario == 0) return false;
            else return true;
        }
        public static bool operator ==(complexo este, double outro)
        {
            if (este.real == outro && este.imaginario == 0) return true;
            else return false;
        }
        //Conjugado
        public static complexo operator ~(complexo a)
        {
            return new complexo(a.imaginario, a.real);
        }
        public double modulo()
        {
            return Math.Sqrt(real * real + imaginario * imaginario);
        }
        public static complexo operator ^(complexo a, int exp)
        {
            if (exp == 0) return new complexo(1, 0);
            else if (exp == 1) return a;
            else return a * a ^ (exp - 1);
        }
    }

    public class matriz
    {
        public int linhas, colunas;
        public List<List<complexo>> elementos;
        /// <summary>
        /// Gera uma matriz de tipo convencional quadrada
        /// </summary>
        /// <param name="tipo">(I)dentidade,(Z)eros,(U)ns</param>
        /// <param name="ordem">Valor de linhas E colunas</param>
        public matriz(char tipo, int ordem)
        {
            linhas = ordem;
            colunas = ordem;
            elementos = new List<List<complexo>>();
            switch (tipo)
            {
                case 'I':
                case 'i':
                    for (int i = 0; i < linhas; i++)
                    {
                        elementos.Add(new List<complexo>());
                        for (int j = 0; j < colunas; j++)
                        {
                            if (j != i) elementos[i].Add(new complexo(0, 0));
                            else elementos[i].Add(new complexo(1, 0));
                        }
                    }
                    break;
                case 'Z':
                case 'z':
                    for (int i = 0; i < linhas; i++)
                    {
                        elementos.Add(new List<complexo>());
                        for (int j = 0; j < colunas; j++) elementos[i].Add(new complexo(0, 0));
                    }
                    break;
                case 'U':
                case 'u':
                    for (int i = 0; i < linhas; i++)
                    {
                        elementos.Add(new List<complexo>());
                        for (int j = 0; j < colunas; j++) elementos[i].Add(new complexo(1, 0));
                    }
                    break;
                case 'A':
                case 'a':
                    Random rand = new Random();
                    for (int i = 0; i < linhas; i++)
                    {
                        elementos.Add(new List<complexo>());
                        for (int j = 0; j < colunas; j++) elementos[i].Add(new complexo(rand.Next(0, ordem * ordem), 0));
                    }
                    break;
                default:

                    break;
            }
        }

        public matriz(int Linhas, int Colunas)
        {
            linhas = Linhas;
            colunas = Colunas;
            elementos = new List<List<complexo>>();
            for (int i = 0; i < linhas; i++)
            {
                elementos.Add(new List<complexo>());
            }
        }

        public matriz copiar(matriz outra)
        {
            this.linhas = outra.linhas;
            this.colunas = outra.colunas;
            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    elementos[i][j] = outra.elementos[i][j];
                }
            }
            return outra;
        }

        public matriz multiplicar(matriz outra)
        {
            if (outra.linhas == this.colunas)
            {
                matriz esta = new matriz(this.linhas, outra.colunas);
                esta.elementos = new List<List<complexo>>();
                for (int i = 0; i < this.linhas; i++)
                {
                    esta.elementos[i] = new List<complexo>();
                    for (int j = 0; j < outra.colunas; j++)
                    {
                        esta.elementos[i].Add(new complexo(0, 0));
                        for (int k = 0; k < this.colunas; k++)
                        {
                            esta.elementos[i][j] += this.elementos[i][k] * outra.elementos[k][j];
                        }
                    }
                }
                return esta;
            }
            else return new matriz('u', outra.colunas);
        }

        public matriz multiplicar(double escalar)
        {
            matriz esta = new matriz(this.linhas, this.colunas);
            esta.elementos = new List<List<complexo>>();
            for (int i = 0; i < this.linhas; i++)
            {
                esta.elementos[i] = new List<complexo>();
                for (int j = 0; j < this.colunas; j++) esta.elementos[i].Add(escalar * this.elementos[i][j]);
            }
            return esta;
        }

        public static matriz operator *(matriz esta, matriz outra) { return esta.multiplicar(outra); }
        public static matriz operator *(matriz esta, double escalar) { return esta.multiplicar(escalar); }

        public void trocaLinha(int linha1, int linha2)
        {
            complexo temp = new complexo(0, 0);
            for (int j = 0; j < this.colunas; j++)
            {
                temp.copiar(elementos[linha1][j]);
                this.elementos[linha1][j].copiar(this.elementos[linha2][j]);
                this.elementos[linha2][j].copiar(temp);
            }
        }

        public void trocaColuna(int coluna1, int coluna2)
        {
            complexo temp;
            for (int j = 0; j < this.linhas; j++)
            {
                temp = this.elementos[coluna1][j];
                this.elementos[coluna1][j] = this.elementos[coluna2][j];
                this.elementos[coluna2][j] = temp;
            }
        }

        public complexo Det()
        {
            complexo D = new complexo(0, 0);
            if (linhas == colunas)
            {
                if (linhas == 2) return elementos[0][0] * elementos[1][1] - elementos[1][0] * elementos[0][1];
                else
                {
                    for (int i = 0; i < colunas; i++)
                    {
                        D += Math.Pow(-1, i) * elementos[0][i] * submatriz(0, i).Det();
                    }
                }
            }
            else//pseudodeterminante
            {

            }
            return D;
        }

        public matriz submatriz(int linha_removida, int coluna_removida)
        {
            matriz sub = new matriz(this.linhas - 1, this.colunas - 1);
            int k = 0, i = 0, j = 0;
            sub.elementos = new List<List<complexo>>();
            while (i < this.linhas)
            {
                if (i != linha_removida)
                {
                    sub.elementos.Add(new List<complexo>());
                    j = 0;
                    while (j < this.colunas)
                    {
                        if (j != coluna_removida) sub.elementos[k].Add(this.elementos[i][j]);
                        j++;
                    }
                    k++;
                }
                i++;
            }
            return sub;
        }
        public static bool igual(matriz esta, matriz outra)
        {
            if (esta.colunas == outra.colunas)
            {
                if (esta.linhas == outra.linhas)
                {
                    for (int i = 0; i < esta.linhas; i++)
                    {
                        for (int j = 0; j < esta.colunas; j++)
                        {
                            if (esta.elementos[i][j] != outra.elementos[i][j])
                                return false;
                        }
                    }
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public void transpor() { this.copiar(this.transposta()); }

        public matriz transposta()
        {
            matriz temp = new matriz(this.colunas, this.linhas);
            for (int i = 0; i < temp.linhas; i++)
            {
                for (int j = 0; j < temp.colunas; j++)
                {
                    temp.elementos[j].Add(this.elementos[i][j]);
                }
            }
            return temp;
        }
        public override String ToString()
        {
            String temp;
            temp = "";
            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    temp += elementos[i][j].ToString() + "\t";
                }
                temp += "\n";
            }
            return temp;
        }
    };

}
