using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiimoteLib;

namespace WiiGL
{
    public class Board
    {
        //declarações de variáveis/objetos e vetores
        public WiimoteState estado;
        public Wiimote balanca;

        private double[] TARA;
        private double[] sensor;
        Sensores tempSensores;

        //definição de funções
        public Board()
        {
            TARA = new double[5];
            sensor = new double[4];
            tempSensores = new Sensores();
        }

        public bool Conectar()
        {
            try
            {
                balanca.Connect();
                balanca.SetReportType(InputReport.Status, true);
                if (balanca.WiimoteState.ExtensionType == ExtensionType.BalanceBoard)
                {
                    TARA = new double[5] { 0, 0, 0, 0, 0 };
                    sensor = new double[5];
                    return true;
                }
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                temp = temp + "\n Siga os seguintes passos: \n" +
                "1 - Verifique se há uma Wii balance Board disponível \n  " +
                "2 - Verifique se o computador está disponível para encontrar dispositivos Bluetooth \n" +
                "3 - Verifique as baterias dela \n" +
                "4 - Caso o LED acenda e as baterias estejam carregadas, deve existir a necessidade de reconectar o dispositivo. \n" +
                "   4.1 - Verifique se o computador está conectado à internet\n " +
                "   4.2 - Inicie o buscador de dispositivos Bluetooth do Windows\n " +
                "   4.3 - Segure o botão dentro do compartimentos de bateria\n " +
                "   4.4 - Ao aparecer a balança na lista de dispositivos, selecione-a, Nintendo RVL-WBC-01\n " +
                "   4.5 - Escolha pareamento sem código\n " +
                "   4.6 - Aguarde duas instalações\n " +
                "   4.7 - Apenas após as instalações, o botão pode ser liberado\n " +
                "   4.8 - A duração da instalação depende da velocidade de conexão\n " +
                "5 - Tente conectar novamente, caso não funcione, repita o processo.";

                System.Windows.MessageBox.Show(temp);
            }
            return false;
        }

        public void Calibrar()
        {
            TARA[0] = estado.BalanceBoardState.SensorValuesKg.TopLeft;
            TARA[1] = estado.BalanceBoardState.SensorValuesKg.TopRight;
            TARA[2] = estado.BalanceBoardState.SensorValuesKg.BottomLeft;
            TARA[3] = estado.BalanceBoardState.SensorValuesKg.BottomRight;
            TARA[4] = estado.BalanceBoardState.WeightKg;
        }

        public Sensores get_sensores()
        {
            sensor[0] = estado.BalanceBoardState.SensorValuesKg.TopLeft - TARA[0];
            sensor[1] = estado.BalanceBoardState.SensorValuesKg.TopRight - TARA[1];
            sensor[2] = estado.BalanceBoardState.SensorValuesKg.BottomLeft - TARA[2];
            sensor[3] = estado.BalanceBoardState.SensorValuesKg.BottomRight - TARA[3];
            sensor[4] = estado.BalanceBoardState.WeightKg - TARA[4];

            for(int i = 0; i < 5; i++)
            {
                if (sensor[i] < 0.5) tempSensores.peso[i] = 0.5;
                else tempSensores.peso[i] = sensor[i];
            }

            return tempSensores;           
        }
    }
    //declaração de estruturas e classes
    public class Centro
    {
        public double x, y;
        public void normalizar(double max) { x /= max; y /= max; }
        public void definir(double X, double Y) { x = X; y = Y; }
        public Centro(double X,double Y)
        {
            definir(X, Y);
        }
        public Centro()
        {
            definir(0, 0);
        }
    }    

    public class Sensores
    {
        Centro tempCentro = new Centro();
        public double[] peso;
        public Sensores()
        {
            peso = new double[5];
        }
        public void normalizar(double max) { for (int i = 0; i < 5; i++) peso[i] /= max; }
        public void definir(double FrEsq, double FrDir, double TrEsq, double TrDir, double To)
        {
            peso[0] = FrEsq;
            peso[1] = FrDir;
            peso[2] = TrEsq;
            peso[3] = TrDir;
            peso[4] = To;
        }
        public void definir(double[] sensor)
        {
            for (int i = 0; i < 5; i++)
            {
                peso[i] = sensor[i];
            }
        }
        public Centro getCentro()
        {
            tempCentro.x = (peso[1] + peso[3] - peso[0] - peso[2]) / peso[4];
            tempCentro.y = (peso[1] + peso[0] - peso[3] - peso[2]) / peso[4];
            return tempCentro;
        }
    }
}
