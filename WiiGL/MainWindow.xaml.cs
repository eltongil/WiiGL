using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using SharpGL.SceneGraph;
using SharpGL;
using System.IO;
using Winforms = System.Windows.Forms;
using System.Text.RegularExpressions;
using Xceed.Wpf.Toolkit;
using Algebra;
using FFT;
using filtros;
using WiimoteLib;

namespace WiiGL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Board balanca;
        public List<Centro> centros;
        public List<Sensores> pesos;
        public List<Centro> centrosFiltro;
        public List<Sensores> pesosFiltro;
        public List<pessoa_reduzido> lista_pacientes;
        private Centro temp, ultimoClick;
        private bool gravando, pronto,nomeEditado;
        private int i, j;
        private double ii, jj;
        private string pasta, arquivo, completo;
        DispatcherTimer Timer;
        double maximoM, maximoG;
        double zoomLim, centro_imagemX, centro_imagemY;
        double tempx, tempy;
        Centro canto1, canto2;
        Sensores temp2;

        public struct cores
        {
            public double verm, verd, azul;
            public cores(double Vermelho, double Verde, double Azul) { verm = Vermelho; verd = Verde; azul = Azul; }
        }

        public void Colorir(cores cor, ref linhaGraf linha)
        {
            linha.cor.azul = cor.azul;
            linha.cor.verm = cor.verm;
            linha.cor.verd = cor.verd;
        }

        public cores Cor(int cor)
        {
            switch (cor)
            {
                case 0:
                    return new cores(1.0, 1.0, 1.0);
                case 1:
                    return new cores(1.0, 1.0, 0.0);
                case 2:
                    return new cores(0.0, 0.0, 1.0);
                case 3:
                    return new cores(0.5, 0.5, 0.5);
                case 4:
                    return new cores(1.0, 0.0, 1.0);
                case 5:
                    return new cores(0.0, 1.0, 0.0);
                case 6:
                    return new cores(1.0, 0.0, 0.0);
                case 7:
                    return new cores(0.0, 1.0, 1.0);
                case 8:
                    return new cores(1.0, 0.5, 0.0);
                default:
                    return new cores(0.0, 0.0, 0.0);
            }
        }
        public cores Cor(string cor) {
            cor.ToLower();
            switch(cor)
            {
                case "branco":
                    return new cores(1.0, 1.0, 1.0);
                case "amarelo":
                    return new cores(1.0, 1.0, 0.0);
                case "azul":
                    return new cores(0.0, 0.0, 1.0);
                case "cinza":
                    return new cores(0.5, 0.5, 0.5);
                case "magenta":
                    return new cores(1.0, 0.0, 1.0);
                case "verde":
                    return new cores(0.0, 1.0, 0.0);
                case "vermelho":
                    return new cores(1.0, 0.0, 0.0);
                case "ciano":
                    return new cores(0.0, 1.0, 1.0);
                case "laranja":
                    return new cores(1.0, 0.5, 0.0);
                default:
                    return new cores(0.0, 0.0, 0.0);
            }
        }

        bool retanZoom;

        public struct linhaGraf
        {
            public cores cor;
            public int tipo;
        }

        linhaGraf[] tracos;
        bool exibirFiltroGeometrico, exibirFiltroLinear;

        Regex Regex_Decimal = new Regex(@"\d+[\.\,]?\d*");

        public void Nomear()
        {
            completo = pasta + "\\" + arquivo + ".plf";
            Endereco.Content = completo;
        }
        
        Pessoa paciente;
        
        public MainWindow()
        {/*
            Tabela_Pessoal();

            //Inicialização automática dos componentes criados no xaml
            zoomLim = 1;
            centro_imagemX = 0;
            centro_imagemY = 0;
            tempx = 0;
            tempy = 0;
            InitializeComponent();
            Ent_Data_Avaliacao.SelectedDate = DateTime.Today;
            gravando = false;//Registra os pontos para gravar proteriormente
            pronto = false;//Anuncia se uma balanca foi encontrada, habilitando outras opções.

            Timer = new DispatcherTimer();
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 63);
            Timer.Start();
            pasta = "C:\\capturas";
            arquivo = "SemNome";
            completo = pasta + "\\" + arquivo + ".plf";
            
            TipoGrafico.SelectedIndex = 0;
            tracos = new linhaGraf[5];
            centros = new List<Centro>();
            pesos = new List<Sensores>();

            zoomLim = 1;
            centro_imagemX = 0;
            centro_imagemY = 0;
            ultimoClick= new Centro();
            canto1 = new Centro();
            canto2 = new Centro();
            temp2 = new Sensores();

            centrosFiltro = new List<Centro>();
            pesosFiltro = new List<Sensores>();
            exibirFiltroGeometrico = false;

            matriz teste = new matriz('A', 5);
            matriz teste2 = new matriz('u', 2);
            testes.Text = teste.ToString();
            testes.Text += teste2.ToString();
            teste.transpor();
            testes.Text += teste.ToString();
            testes.Text += teste.Det();
            
            Ler_Pacientes();*/
        }
        
        void Tabela_Pessoal()
        {/*
            paciente = new Pessoa();
            paciente.carregar_listas();*/
        }

        void Timer_Tick(object sender, EventArgs e)
        {/*
            if (gravando)
            {               
                pesos.Add(balanca.get_sensores());
                temp = pesos[pesos.Count-1].getCentro();
                CGXLabel.Content = temp.x.ToString();
                CGYLabel.Content = temp.y.ToString();
                Massa_Total.Content = pesos[pesos.Count-1].peso[4].ToString() + " Kg totais";
                centros.Add(temp);
            }
            if (pronto) NvBateria.Value = balanca.estado.Battery;*/
        }

        private void openGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {/*
            OpenGL gl = openGLControl.OpenGL;

            gl.LoadIdentity();
            if (pronto)
            {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                j = centros.Count;
                gl.Begin(OpenGL.GL_LINE_STRIP);
                for (i = 0; i < j; i++)
                {
                    ii = i; jj = j;
                    gl.Color(ii / jj, 0, 1 - ii / jj);
                    gl.Vertex(centros[i].x / 2, centros[i].y / 2, 2);
                }
                gl.End();
                Sai_Num_Capturas.Content = j.ToString() + " Quadros.";
            }*/
        }
    
        private void openGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            //OpenGL gl = openGLControl.OpenGL;
            //gl.ClearColor(0, 0, 0, 0);
        }

        private void openGLControl_Resized(object sender, OpenGLEventArgs args)
        {/*
            OpenGL gl = openGLControl.OpenGL;//  Get the OpenGL object.
            gl.MatrixMode(OpenGL.GL_PROJECTION);//  Set the projection matrix.
            gl.LoadIdentity();//  Load the identity.
            gl.Perspective(60, 1, 1, 5);//  Create a perspective transformation.
            gl.LookAt(0, 0, 5, 0, 0, 0, 0, 1, 0);//  Use the 'look at' helper function to position and aim the camera.
            gl.MatrixMode(OpenGL.GL_MODELVIEW); //  Set the modelview matrix.*/
        }

        private void Ativador_Click(object sender, RoutedEventArgs e)
        {/*
            if (gravando)
            {
                gravando = false;
                Buscador.Content = "Balança pronta.";
                Ativador.Content = "Ativar";
            }
            else
            {
                gravando = true;
                Buscador.Content = "Gravando...";
                Ativador.Content = "Desativar";
            }*/
        }

        private void LoadWiimote()//Procura uma balanca, se não encontrar fecha o programa.
        {/*
            balanca = new Board();
            Buscador.Content = "Preparando...";
            pronto = balanca.Conectar();
            if (pronto)
            {
                Ativador.IsEnabled = true;
                Buscador.Content = "Pronto";
            }
            else
            {
                Buscador.Content = "Buscar";
            }
            */
        }

        private void Calibrar_Click(object sender, RoutedEventArgs e)
        {
            //balanca.Calibrar();
        }

        private void folderSel_Click(object sender, RoutedEventArgs e)
        {/*
            Winforms.FolderBrowserDialog dialog = new Winforms.FolderBrowserDialog();
            Winforms.DialogResult result = dialog.ShowDialog();
            if (result == Winforms.DialogResult.OK)
            {
                pasta = dialog.SelectedPath;
                Nomear();
            }*/
        }

        private void TextChange(object sender, TextChangedEventArgs e)
        {/*
            arquivo = NomeEdit.Text;
            Nomear();
            nomeEditado = true;*/      
        }

        private void salve_Click(object sender, RoutedEventArgs e)
        {/*
            Mouse.SetCursor(Cursors.Wait);
            if (gravando) Ativador_Click(sender, e);
            if (!Directory.Exists("../Dados/pid"))
                Directory.CreateDirectory("../Dados/pid");
            if(!nomeEditado) completo = "../Dados/pid/" + paciente.Nome + "/" + DateTime.Now.ToString() + ".txt";
            StreamWriter sai = new StreamWriter(completo);

            sai.Write("-Captura de centro de massa" + sai.NewLine);
            sai.Write("-" + DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString() + sai.NewLine);
            sai.Write("-" + DateTime.Now.Hour + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + sai.NewLine);
            sai.Write("- TopLeft  TopRight   BotLeft BotRight Total GCX GCY" + sai.NewLine);
            for (i = 0; i < pesos.Count; i++)
            {
                sai.Write(i.ToString() + " ");
                sai.Write(pesos[i].peso[0] + "  " + pesos[i].peso[1] + "  " + pesos[i].peso[2] + "  " + pesos[i].peso[3] + "  " + pesos[i].peso[4] + "  ");
                sai.Write(" -- " + centros[i].x + " " + centros[i].y + sai.NewLine);
            }
            Mouse.SetCursor(Cursors.Arrow);*/
        }

        private void normalizar()
        {/*
            maximoG = centros[0].x;
            maximoM = pesos[0].peso[0];
            for (i = 0; i < centros.Count; i++)
            {
                if (centros[i].x * centros[i].x > maximoG * maximoG) maximoG = centros[i].x > 0 ? centros[i].x : centros[i].x * (-1);
                if (centros[i].y * centros[i].y > maximoG * maximoG) maximoG = centros[i].y > 0 ? centros[i].y : centros[i].y * (-1);
                for (j = 0; j < 5; j++) if (pesos[i].peso[j] * pesos[i].peso[j] > maximoM * maximoM) maximoM = pesos[i].peso[j] > 0 ? pesos[i].peso[j] : pesos[i].peso[j] * (-1);
            }*/
        }

        /// <summary>
        /// Normaliza a imagem segundo um único parametro.
        /// </summary>
        /// <param name="especificar">String CENTROX, CENTROY, peso[1]ENTE_ESQ, FRENTE_DIR, TRAS_ESQ, TRAS_DIR, TOTAL</param>
        private void normalizar(String especificar)
        {/*
            switch (especificar)
            {
                case "CENTROX":
                    maximoG = centros[0].x;
                    for (i = 0; i < centros.Count; i++)
                        if (centros[i].x * centros[i].x > maximoG * maximoG) maximoG = centros[i].x > 0 ? centros[i].x : centros[i].x * (-1);
                    zoomLim = maximoG;
                    break;
                case "CENTROY":
                    maximoG = centros[0].y;
                    for (i = 0; i < centros.Count; i++)
                        if (centros[i].y * centros[i].y > maximoG * maximoG) maximoG = centros[i].y > 0 ? centros[i].y : centros[i].y * (-1);
                    zoomLim = maximoG;
                    break;
                case "FRENTE_ESQ":
                    maximoM = pesos[0].peso[0];
                    for (i = 0; i < pesos.Count; i++)
                        if (pesos[i].peso[0] * pesos[i].peso[0] > maximoM * maximoM) maximoM = pesos[i].peso[0] > 0 ? pesos[i].peso[0] : pesos[i].peso[0] * (-1);
                    zoomLim = maximoM;
                    break;
                case "FRENTE_DIR":
                    maximoM = pesos[0].peso[1];
                    for (i = 0; i < pesos.Count; i++)
                        if (pesos[i].peso[1] * pesos[i].peso[1] > maximoM * maximoM) maximoM = pesos[i].peso[1] > 0 ? pesos[i].peso[1] : pesos[i].peso[1] * (-1);
                    zoomLim = maximoM;
                    break;
                case "TRAS_ESQ":
                    maximoM = pesos[0].peso[2];
                    for (i = 0; i < pesos.Count; i++)
                        if (pesos[i].peso[2] * pesos[i].peso[2] > maximoM * maximoM) maximoM = pesos[i].peso[2] > 0 ? pesos[i].peso[2] : pesos[i].peso[2] * (-1);
                    zoomLim = maximoM;
                    break;
                case "TRAS_DIR":
                    maximoM = pesos[0].peso[3];
                    for (i = 0; i < pesos.Count; i++)
                        if (pesos[i].peso[3] * pesos[i].peso[3] > maximoM * maximoM) maximoM = pesos[i].peso[3] > 0 ? pesos[i].peso[3] : pesos[i].peso[3] * (-1);
                    zoomLim = maximoM;
                    break;
                case "TOTAL":
                    maximoM = pesos[0].peso[4];
                    for (i = 0; i < pesos.Count; i++)
                        if (pesos[i].peso[4] * pesos[i].peso[4] > maximoM * maximoM) maximoM = pesos[i].peso[4] > 0 ? pesos[i].peso[4] : pesos[i].peso[4] * (-1);
                    zoomLim = maximoM;
                    break;
            }*/
        }

        private void Grafico_OpenGLDraw(object sender, OpenGLEventArgs args)
        {/*
            OpenGL gl = Grafico.OpenGL;
            gl.LoadIdentity();
            atualizar_Grafico();
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            if (TipoGrafico.SelectedIndex == 0)
            {
                j = centros.Count;
                jj = j;
                gl.Begin(OpenGL.GL_LINE_STRIP);
                for (i = 0; i < j; i++)
                {
                    ii = i;
                    gl.Color(ii / jj, 0, 1.0 - ii / jj);
                    gl.Vertex(centros[i].x, centros[i].y, 0);
                }
                gl.End();
                if (exibirFiltroGeometrico && centrosFiltro.Count > 2)
                {
                    gl.Begin(OpenGL.GL_LINE_STRIP);
                    gl.Color(1.0, 1.0, 1.0);
                    for (i = 0; i < j; i++)
                    {
                        ii = i;
                        gl.Vertex(centrosFiltro[i].x, centrosFiltro[i].y, 0);
                    }
                    gl.End();
                }
                Sai_Num_Capturas.Content = j.ToString();
            }
            else if (TipoGrafico.SelectedIndex == 1)
            {
                j = centros.Count;
                jj = j;
                if (CBtraco0.IsChecked == true) { desenhaLinha(centros, 0, "-"); }
                if (CBtraco1.IsChecked == true) { desenhaLinha(centros, 1, "-"); }
                if (exibirFiltroGeometrico && centrosFiltro.Count > 2)
                {
                    if (CBtraco0.IsChecked == true) { desenhaLinha(centrosFiltro, 0, "--"); }
                    if (CBtraco1.IsChecked == true) { desenhaLinha(centrosFiltro, 1, "--"); }
                }
            }
            else
            {
                j = pesos.Count;
                jj = j;
                if (CBtraco0.IsChecked == true) { desenhaLinha(pesos, 0, "-"); }
                if (CBtraco1.IsChecked == true) { desenhaLinha(pesos, 1, "-"); }
                if (CBtraco2.IsChecked == true) { desenhaLinha(pesos, 2, "-"); }
                if (CBtraco3.IsChecked == true) { desenhaLinha(pesos, 3, "-"); }
                if (CBtraco4.IsChecked == true) { desenhaLinha(pesos, 4, "-"); }
                if (exibirFiltroLinear && pesosFiltro.Count > 2)
                {
                    if (CBtraco0.IsChecked == true) { desenhaLinha(pesosFiltro, 0, "--"); }
                    if (CBtraco1.IsChecked == true) { desenhaLinha(pesosFiltro, 1, "--"); }
                    if (CBtraco2.IsChecked == true) { desenhaLinha(pesosFiltro, 2, "--"); }
                    if (CBtraco3.IsChecked == true) { desenhaLinha(pesosFiltro, 3, "--"); }
                    if (CBtraco4.IsChecked == true) { desenhaLinha(pesosFiltro, 4, "--"); }
                }
            }
            //Desenhos permanentes
            if (!retanZoom)
            {
                gl.Color(0.0, 1.0, 0.0);
                gl.Begin(OpenGL.GL_LINE_LOOP);//Retangulo de zoom
                gl.Vertex(canto1.x, canto1.y, 0.0);
                gl.Vertex(canto2.x, canto1.y, 0.0);
                gl.Vertex(canto2.x, canto2.y, 0.0);
                gl.Vertex(canto1.x, canto2.y, 0.0);
                gl.End();
            }

            gl.PointSize(5.0f);
            gl.Begin(OpenGL.GL_POINTS);//último ponto a receber click direito.
            gl.Color(0.0, 1.0, 0.0);
            gl.Vertex(ultimoClick.x, ultimoClick.y, 0.0);
            gl.End();*/
        }
        /// <summary>
        /// Desenha linha para centro de gravidade linear
        /// </summary>
        /// <param name="exibido">Sobrecarga, centros de gravida</param>
        /// <param name="grandeza">0 para x, 1 para y</param>
        /// <param name="traco">- continua, -- tracejada, . pontos</param>
        private void desenhaLinha(List<Centro> exibido, int grandeza, String traco)
        {/*
            OpenGL gl = Grafico.OpenGL;
            if (traco == "-") gl.Begin(OpenGL.GL_LINE_STRIP);
            if (traco == "--") gl.Begin(OpenGL.GL_LINES);
            if (traco == ".") gl.Begin(OpenGL.GL_POINTS);
            gl.Color(tracos[grandeza].cor.verm, tracos[grandeza].cor.verd, tracos[grandeza].cor.azul);
            if (grandeza == 0) for (ii = 0; ii < j; ii++) { gl.Vertex((ii * 2 / j) * maximoG - maximoG, exibido[(int)ii].x, 0); }
            else for (ii = 0; ii < j; ii++) { gl.Vertex((ii * 2 / j) * maximoG - maximoG, exibido[(int)ii].y, 0); }
            gl.End();*/
        }
        /// <summary>
        /// Desenha linha para histórico de pesos por sensor
        /// </summary>
        /// <param name="exibido">Sobrecarga, pesos</param>
        /// <param name="grandeza">0 para frente esq. 1 para frente dir. 2 para trás esq. 3 para trás dir. 4 para total</param>
        /// <param name="traco">- continua, -- tracejada, . pontos</param>
        private void desenhaLinha(List<Sensores> exibido, int grandeza, String traco)
        {/*
            OpenGL gl = Grafico.OpenGL;
            if (traco == "-") gl.Begin(OpenGL.GL_LINE_STRIP);
            if (traco == "--") gl.Begin(OpenGL.GL_LINES);
            if (traco == ".") gl.Begin(OpenGL.GL_POINTS);
            gl.Color(tracos[grandeza].cor.verm, tracos[grandeza].cor.verd, tracos[grandeza].cor.azul);
            for (ii = 0; ii < j; ii++) { gl.Vertex((ii * 2 / j) * maximoM - maximoM, exibido[(int)ii].peso[grandeza] * 2 - maximoM, 0); }
            gl.End();*/
        }
        private void Grafico_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
           // OpenGL gl = Grafico.OpenGL;
           // gl.ClearColor(0, 0, 0, 0);
        }

        private void Grafico_Resized(object sender, OpenGLEventArgs args)
        {
            //atualizar_Grafico();
        }

        private void Abrir_Click(object sender, RoutedEventArgs e)
        {/*
            Winforms.OpenFileDialog openFile = new Winforms.OpenFileDialog();
            if (openFile.ShowDialog() == Winforms.DialogResult.OK)
            {
                centros.Clear();
                pesos.Clear();
                StreamReader entra = new StreamReader(openFile.FileName);
                String linha;
                String[] valores = new String[7];
                Sensores temp2 =  new Sensores();

                while (!entra.EndOfStream)
                {
                    linha = entra.ReadLine();
                    if (!linha.StartsWith("-"))
                    {
                        valores = linha.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        for (i = 0; i < 5; i++) temp2.peso[i] = double.Parse(valores[1 + i]);
                        temp.x = double.Parse(valores[7]);
                        temp.y = double.Parse(valores[8]);

                        pesos.Add(temp2);
                        centros.Add(temp);
                    }
                }
                numFrames.Content = pesos.Count.ToString();
                normalizar();
                sliderMediaSimples.Maximum = pesos.Count;
                sliderMediaBid1.Maximum = pesos.Count;
                sliderMediaBid2.Maximum = pesos.Count;
                atualizar_Grafico();
            }*/
        }

        private void TipoGrafico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {/*
            Grafico.OpenGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            switch (TipoGrafico.SelectedIndex)
            {
                case 0:
                    zoomLim = maximoG;
                    centro_imagemX = 0;
                    centro_imagemY = 0;
                    CBtraco0.IsEnabled = false;
                    CorTraco0.IsEnabled = false;
                    CBtraco1.IsEnabled = false;
                    CorTraco1.IsEnabled = false;
                    CBtraco2.IsEnabled = false;
                    CorTraco2.IsEnabled = false;
                    CBtraco3.IsEnabled = false;
                    CorTraco3.IsEnabled = false;
                    CBtraco4.IsEnabled = false;
                    CorTraco4.IsEnabled = false;
                    LinhaBase.IsEnabled = false;
                    break;
                case 1:
                    zoomLim = maximoG;
                    centro_imagemX = 0;
                    centro_imagemY = 0;
                    CBtraco0.IsEnabled = true;
                    CBtraco0.Content = "Mediolateral";
                    CBtraco0.IsChecked = true;
                    Colorir(Cor("branco"), ref tracos[0]);
                    CorTraco0.IsEnabled = true;
                    CorTraco0.SelectedIndex = 0;
                    CBtraco1.IsEnabled = true;
                    CBtraco1.IsChecked = true;
                    CBtraco1.Content = "Anteroposterior";
                    CorTraco1.SelectedIndex = 2;
                    Colorir(Cor("Azul"), ref tracos[1]);
                    CorTraco1.IsEnabled = true;
                    CBtraco2.IsEnabled = false;
                    CorTraco2.IsEnabled = false;
                    CBtraco3.IsEnabled = false;
                    CorTraco3.IsEnabled = false;
                    CBtraco4.IsEnabled = false;
                    CorTraco4.IsEnabled = false;
                    LinhaBase.IsEnabled = true;
                    break;
                case 2:
                    zoomLim = maximoM;
                    CBtraco0.IsEnabled = true;
                    CBtraco0.Content = "Frente Esq.";
                    CBtraco0.IsChecked = true;
                    CorTraco0.IsEnabled = true;
                    CorTraco0.IsEnabled = true;
                    CorTraco0.SelectedIndex = 0;
                    CBtraco1.IsEnabled = true;
                    CBtraco1.Content = "Frente Dir.";
                    CBtraco1.IsChecked = true;
                    CorTraco1.IsEnabled = true;
                    CorTraco1.SelectedIndex = 1;
                    CBtraco2.IsEnabled = true;
                    CBtraco2.Content = "Traz Esq.";
                    CBtraco2.IsChecked = true;
                    CorTraco2.IsEnabled = true;
                    CorTraco2.SelectedIndex = 2;
                    CBtraco3.IsEnabled = true;
                    CBtraco3.Content = "Traz Dir.";
                    CBtraco3.IsChecked = true;
                    CorTraco3.IsEnabled = true;
                    CorTraco3.SelectedIndex = 3;
                    CBtraco4.IsEnabled = true;
                    CBtraco4.Content = "Massa Total";
                    CBtraco4.IsChecked = true;
                    CorTraco4.IsEnabled = true;
                    CorTraco4.SelectedIndex = 4;
                    break;
            }*/
        }

        private void Buscador_Click(object sender, RoutedEventArgs e)
        {/*
            Buscador.Content = "Procurando...";            
            LoadWiimote();*/
        }

        private void Window_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {/*
            tabs.Height = JanelaMain.ActualHeight - 50;
            tabs.Width = JanelaMain.ActualWidth - 25;
            //atualizar_Grafico();
            tabFiltros.Width = tabs.ActualWidth - 35;*/
        }
        
        private void JanelaMain_StateChanged_1(object sender, EventArgs e)
        {/*
            tabs.Height = JanelaMain.ActualHeight - 50;
            tabs.Width = JanelaMain.ActualWidth - 25;
            //atualizar_Grafico();
            tabFiltros.Width = tabs.ActualWidth - 35;*/
        }
        
        private void CorTraco_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {/*
            string nome = ((ComboBox)sender).Name;

            i = int.Parse(nome.Remove(0, 8));
            Colorir(Cor(((ComboBox)sender).SelectedIndex),ref tracos[i]);*/
        }

        private void Zoom(object sender, MouseWheelEventArgs e)
        {/*
            double temp = e.Delta, temp2 = zoomLim;
            zoomLim += zoomLim * temp / 1200.0;
            centro_imagemX += 2 * (temp2 - zoomLim) * (e.GetPosition(Grafico).X - Grafico.ActualWidth / 2) / Grafico.ActualWidth;
            centro_imagemY -= 2 * (temp2 - zoomLim) * (e.GetPosition(Grafico).Y - Grafico.ActualHeight / 2) / Grafico.ActualHeight;
            if (zoomLim < 0) zoomLim = 0.1;
            atualizar_Grafico();*/
        }

        private void resetZoom_Click(object sender, RoutedEventArgs e)
        {/*
            if (TipoGrafico.SelectedIndex == 2) zoomLim = maximoM;
            else zoomLim = maximoG;
            centro_imagemX = 0;
            centro_imagemY = 0;
            espessuraLinha.Value = 1;
            atualizar_Grafico();*/
        }
        /// <summary>
        /// Função que relaciona o mouse com o zoom e translado, sinal do próprio dispositivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grafico_MouseMove(object sender, MouseEventArgs e)
        {/*
            //Translada a imagem segurando o botão esquerdo do mouse.
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                centro_imagemX -= zoomLim * (e.GetPosition(Grafico).X - tempx) / Grafico.ActualWidth;
                centro_imagemY += zoomLim * (e.GetPosition(Grafico).Y - tempy) / Grafico.ActualHeight;
                atualizar_Grafico();
            }
            //Inicio da função zoom por retângulo, segurando o botão central do mouse (rolo)
            tempx = e.GetPosition(Grafico).X;
            tempy = e.GetPosition(Grafico).Y;
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                if (retanZoom)//canto inicial do retângulo
                {//A equação usada nos cantos converte a posição do mouse em relação a janela gráfica nos parâmetros da visualização OpenGL
                    canto1.x = centro_imagemX + 2 * zoomLim * (e.GetPosition(Grafico).X - Grafico.ActualWidth / 2) / Grafico.ActualWidth;
                    canto1.y = centro_imagemY - 2 * zoomLim * (e.GetPosition(Grafico).Y - Grafico.ActualHeight / 2) / Grafico.ActualHeight;
                    retanZoom = false;//Próximo ponto já não é o inicial
                }
                else
                {//canto final atual do retângulo
                    canto2.x = centro_imagemX + 2 * zoomLim * (e.GetPosition(Grafico).X - Grafico.ActualWidth / 2) / Grafico.ActualWidth;
                    canto2.y = centro_imagemY - 2 * zoomLim * (e.GetPosition(Grafico).Y - Grafico.ActualHeight / 2) / Grafico.ActualHeight;
                }
            }
            //Ao soltar o botão central, o retângulo é apagado e a imagem é concentrada em sua área
            if (e.MiddleButton == MouseButtonState.Released && retanZoom == false)
            {
                retanZoom = true;
                centro_imagemX = (canto2.x + canto1.x) / 2;
                centro_imagemY = (canto2.y + canto1.y) / 2;
                tempx = canto1.x > canto2.x ? canto1.x - canto2.x : canto2.x - canto1.x;
                tempy = canto1.y > canto2.y ? canto1.y - canto2.y : canto2.y - canto1.y;
                zoomLim = (tempx > tempy ? tempx : tempy) / 2;
                atualizar_Grafico();
            }*/
        }

        //refresh screen
        private void atualizar_Grafico()
        {/*
            OpenGL gl = Grafico.OpenGL;
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho(-zoomLim + centro_imagemX, zoomLim + centro_imagemX, -zoomLim + centro_imagemY, zoomLim + centro_imagemY, -2, 5);
            gl.LookAt(0, 0, 5, 0, 0, 0, 0, 1, 0);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();*/
        }

        //Mudar a espessura na linha
        private void espessuraLinha_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Grafico.OpenGL.LineWidth((float)e.NewValue);
            //espLinha.Content = "Espessura: " + e.NewValue.ToString();
        }

        //Informações de um ponto clicado com o botão direito do mouse. Segundo o tipo de gráfico em uso.
        private void Grafico_MouseRightButtonDown_1(object sender, MouseButtonEventArgs e)
        {/*
            if (TipoGrafico.SelectedIndex == 0)
            {
                pontoX.Content = "X: " + (centro_imagemX + 2 * zoomLim * (e.GetPosition(Grafico).X - Grafico.ActualWidth / 2) / Grafico.ActualWidth).ToString();
                pontoY.Content = "Y: " + (centro_imagemY - 2 * zoomLim * (e.GetPosition(Grafico).Y - Grafico.ActualHeight / 2) / Grafico.ActualHeight).ToString();
            }
            else if (TipoGrafico.SelectedIndex == 1)
            {
                pontoX.Content = "Frame: " + ((int)(Grafico.ActualWidth * pesos.Count / e.GetPosition(Grafico).X)).ToString();
                pontoY.Content = "Y: " + (centro_imagemY - 2 * zoomLim * (e.GetPosition(Grafico).Y - Grafico.ActualHeight / 2) * zoomLim / Grafico.ActualHeight).ToString();
            }
            else
            {
                pontoX.Content = "Frame: " + ((int)(Grafico.ActualWidth * pesos.Count / e.GetPosition(Grafico).X)).ToString();
                pontoY.Content = "Y: " + (zoomLim * (1 - e.GetPosition(Grafico).Y / Grafico.ActualHeight)).ToString();
            }
            ultimoClick.definir(centro_imagemX + 2 * zoomLim * ((e.GetPosition(Grafico).X - Grafico.ActualWidth / 2) / Grafico.ActualWidth), centro_imagemY - 2 * zoomLim * ((e.GetPosition(Grafico).Y - Grafico.ActualHeight / 2) / Grafico.ActualHeight));
            */
        }

        //Centraliza e exibe o gráfico segundo uma característica selecionada.
        private void MudarBase(object sender, SelectionChangedEventArgs e)
        {/*
            switch (((ComboBox)sender).SelectedIndex)
            {
                case 0:
                    normalizar();
                    break;
                case 1:
                    if (TipoGrafico.SelectedIndex == 1) normalizar("CENTROX");
                    else if (TipoGrafico.SelectedIndex == 2) normalizar("FRENTE_ESQ");
                    break;
                case 2:
                    if (TipoGrafico.SelectedIndex == 1) normalizar("CENTROY");
                    else if (TipoGrafico.SelectedIndex == 2) normalizar("FRENTE_DIR");
                    break;
                case 3:
                    if (TipoGrafico.SelectedIndex == 1) ((ComboBox)sender).SelectedIndex = 0;
                    if (TipoGrafico.SelectedIndex == 2) normalizar("TRAS_ESQ");
                    break;
                case 4:
                    if (TipoGrafico.SelectedIndex == 1) ((ComboBox)sender).SelectedIndex = 0;
                    if (TipoGrafico.SelectedIndex == 2) normalizar("TRAS_DIR");
                    break;
                case 5:
                    if (TipoGrafico.SelectedIndex == 1) ((ComboBox)sender).SelectedIndex = 0;
                    if (TipoGrafico.SelectedIndex == 2) normalizar("TOTAL");
                    break;
            }*/
        }

        //Variações do gráfico de médias simples.
        private void sliderMediaSimples_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {/*
            int N = (int)e.NewValue;
            labelN.Content = "N = " + N.ToString();
            switch (TipoGrafico.SelectedIndex)
            {
                case 0:
                case 1:
                    Centro temporario = new Centro();
                    centrosFiltro.Clear();
                    for (i = 0; i < centros.Count; i++)
                    {
                        temporario.definir(0, 0);
                        for (j = i; j >= 0 && j >= i - N; j--)
                        {
                            temporario.definir(temporario.x + centros[j].x, temporario.y + centros[j].y);
                        }
                        temporario.definir(temporario.x / (N + 1), temporario.y / (N + 1));
                        centrosFiltro.Add(temporario);
                    }
                    exibirFiltroGeometrico = true;
                    break;
                case 2:
                    Sensores temporario2 = new Sensores();
                    pesosFiltro.Clear();
                    for (i = 0; i < centros.Count; i++)
                    {
                        temporario2.definir(0, 0, 0, 0, 0);
                        for (j = i; j >= 0 && j >= i - N; j--)
                        {
                            temporario2.definir(temporario2.peso[0] + pesos[j].peso[0], temporario2.peso[1] + pesos[j].peso[1], temporario2.peso[2] + pesos[j].peso[2], temporario2.peso[3] + pesos[j].peso[3], temporario2.peso[4] + pesos[j].peso[4]);
                        }
                        temporario2.normalizar((double)N + 1);
                        pesosFiltro.Add(temporario2);
                    }
                    exibirFiltroLinear = true;
                    break;
            }*/
        }

        private void sliderMediaBid1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {/*
            int Na = (int)e.NewValue;
            int Np = (int)sliderMediaBid2.Value;
            labelNa2.Content = "Na = " + e.NewValue.ToString();
            sliderMediaBid2.Value = e.NewValue;
            switch (TipoGrafico.SelectedIndex)
            {
                case 0:
                case 1:
                    Centro temporario = new Centro();
                    centrosFiltro.Clear();
                    for (i = 0; i < centros.Count; i++)
                    {
                        temporario.definir(0, 0);
                        for (j = i - Na > 0 ? i - Na : 0; j <= i + Np && j < centros.Count; j++)
                        {
                            temporario.definir(temporario.x + centros[j].x, temporario.y + centros[j].y);
                        }
                        temporario.definir(temporario.x / (Na + Np + 1), temporario.y / (Na + Np + 1));
                        centrosFiltro.Add(temporario);
                    }
                    exibirFiltroGeometrico = true;
                    break;
                case 2:
                    Sensores temporario2 = new Sensores();
                    pesosFiltro.Clear();
                    for (i = 0; i < centros.Count; i++)
                    {
                        temporario2.definir(0, 0, 0, 0, 0);
                        for (j = i - Na > 0 ? i - Na : 0; j <= i + Np && j < pesos.Count; j++)
                        {
                            temporario2.definir(temporario2.peso[0] + pesos[j].peso[0], temporario2.peso[1] + pesos[j].peso[1], temporario2.peso[2] + pesos[j].peso[2], temporario2.peso[3] + pesos[j].peso[3], temporario2.peso[4] + pesos[j].peso[4]);
                        }
                        temporario2.normalizar(Na + Np + 1);
                        pesosFiltro.Add(temporario2);
                    }
                    exibirFiltroLinear = true;
                    break;
            }*/
        }

        private void sliderMediaBid2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {/*
            int Np = (int)e.NewValue;
            int Na = (int)sliderMediaBid1.Value;
            labelNp2.Content = "Np = " + Np;
            switch (TipoGrafico.SelectedIndex)
            {
                case 0:
                case 1:
                    Centro temporario = new Centro();
                    centrosFiltro.Clear();
                    for (i = 0; i < centros.Count; i++)
                    {
                        temporario.definir(0, 0);
                        for (j = i - Na > 0 ? i - Na : 0; j <= i + Np && j < centros.Count; j++)
                        {
                            temporario.definir(temporario.x + centros[j].x, temporario.y + centros[j].y);
                        }
                        temporario.definir(temporario.x / (Na + Np + 1), temporario.y / (Na + Np + 1));
                        centrosFiltro.Add(temporario);
                    }
                    exibirFiltroGeometrico = true;
                    break;
                case 2:
                    Sensores temporario2 = new Sensores();
                    pesosFiltro.Clear();
                    for (i = 0; i < centros.Count; i++)
                    {
                        temporario2.definir(0, 0, 0, 0, 0);
                        for (j = i - Na > 0 ? i - Na : 0; j <= i + Np && j < pesos.Count; j++)
                        {
                            temporario2.definir(temporario2.peso[0] + pesos[j].peso[0], temporario2.peso[1] + pesos[j].peso[1], temporario2.peso[2] + pesos[j].peso[2], temporario2.peso[3] + pesos[j].peso[3], temporario2.peso[4] + pesos[j].peso[4]);
                        }
                        temporario2.normalizar(Na + Np + 1);
                        pesosFiltro.Add(temporario2);
                    }
                    exibirFiltroLinear = true;
                    break;
            }*/
        }
        
        private void So_Numeros(object sender, TextCompositionEventArgs e) { e.Handled = Regex_Decimal.IsMatch(e.Text); }
        
        private void AplicarSG_Click(object sender, RoutedEventArgs e)
        {/*
            int N = int.Parse(SGN.Text);
            int M = int.Parse(SGM.Text);
            int K = int.Parse(SGK.Text);
            int I = int.Parse(SGI.Text);
            if (N <= 0)
            {
                ErroGolay.Content = "Valor de N invalido.";
            }
            else if (M <= 0)
            {
                ErroGolay.Content = "Valor de M invalido.";
            }
            else if (K < 0)
            {
                ErroGolay.Content = "Valor de K invalido.";
            }
            if (I < 1) I = 1;
            else
            {
                ErroGolay.Content = "Processando";
                double[,] matriz = new double[K, K];//Matrix de variáveis
                double[] variaveis = new double[N + M];
                int linha, coluna, linha2;
                double pivo;
                if (TipoGrafico.SelectedIndex != 2)
                {
                    double[,] a = new double[K, 2]; //coeficiente
                    double[,] y = new double[K, 2];//Valores conhecidos
                    centrosFiltro.Clear();
                    for (i = 0; i < centros.Count; i++)
                    {
                        for (linha = 0; linha < K; linha++)
                        {
                            for (coluna = 0; coluna < K; coluna++)
                            {
                                matriz[linha, coluna] = 0;
                                for (j = i - N > 0 ? i - N : 0; j < i + M && j < centros.Count; j++)
                                {
                                    matriz[linha, coluna] += Math.Pow(j, linha + coluna);
                                }
                            }
                            y[linha, 0] = 0;
                            y[linha, 1] = 0;
                            for (j = i - N > 0 ? i - N : 0; j < i + M && j < centros.Count; j++)
                            {
                                y[linha, 0] += centros[j].x * Math.Pow(j, linha);
                                y[linha, 1] += centros[j].y * Math.Pow(j, linha);
                            }
                        }
                        //Resolução do sistema via método de Jordan
                        //Eliminação gaussiana progressiva e retroativa com fim em uma Matriz identidade
                        for (linha = 0; linha < K; linha++)
                        {
                            for (linha2 = 0; linha2 < K; linha2++)
                            {
                                if (linha2 != linha)
                                {
                                    pivo = matriz[linha, linha] / matriz[linha2, linha];
                                    y[linha2, 0] -= y[linha, 0] * pivo;
                                    y[linha2, 1] -= y[linha, 1] * pivo;
                                    for (coluna = 0; coluna < K; coluna++)
                                    {
                                        matriz[linha2, coluna] -= matriz[linha, coluna] * pivo;
                                    }
                                }
                            }
                        }
                        for (linha = 0; linha < K; linha++)
                        {
                            for (coluna = 0; coluna < K; coluna++)
                            {
                                //testes.Text += matriz[linha, coluna] + " ";
                            }
                            //testes.Text += y[linha,0] +" "+ y[linha,1] + "\n";
                        }
                        //Solução progressiva
                        for (int l = 0; l < I; l++)
                        {
                            temp.definir(0, 0);
                            for (j = 0; j < K; j++)
                            {
                                a[j, 0] = y[j, 0] / matriz[j, j];
                                a[j, 1] = y[j, 1] / matriz[j, j];
                                temp.x += a[j, 0] * Math.Pow(i, j);
                                temp.y += a[j, 1] * Math.Pow(i, j);
                            }
                            centrosFiltro.Add(temp);
                        }
                    }
                    exibirFiltroGeometrico = true;
                }
                else
                {
                    double[,] a = new double[K, 5]; //coeficiente
                    double[,] y = new double[K, 5];//Valores conhecidos
                    pesosFiltro.Clear();
                    for (i = 0; i < centros.Count; i++)
                    {
                        for (linha = 0; linha < K; linha++)
                        {
                            y[linha, 0] = 0;
                            y[linha, 1] = 0;
                            y[linha, 2] = 0;
                            y[linha, 3] = 0;
                            y[linha, 4] = 0;
                            for (j = i - N > 0 ? i - N : 0; j < i + M && j < centros.Count; j++)
                            {
                                y[linha, 0] += pesos[j].peso[0] * Math.Pow(j, linha);
                                y[linha, 1] += pesos[j].peso[1] * Math.Pow(j, linha);
                                y[linha, 2] += pesos[j].peso[2] * Math.Pow(j, linha);
                                y[linha, 3] += pesos[j].peso[3] * Math.Pow(j, linha);
                                y[linha, 4] += pesos[j].peso[4] * Math.Pow(j, linha);
                            }
                            for (coluna = 0; coluna < K; coluna++)
                            {
                                matriz[linha, coluna] = 0;
                                for (j = i - N > 0 ? i - N : 0; j < i + M && j < centros.Count; j++)
                                {
                                    matriz[linha, coluna] += Math.Pow(j, linha + coluna);
                                }
                            }
                        }
                        //Resolução do sistema via método de Jordan
                        //Eliminação gaussiana progressiva e retroativa com fim em uma Matriz identidade
                        for (linha = 0; linha < K; linha++)
                        {
                            for (linha2 = 0; linha2 < K; linha2++)
                            {
                                if (linha2 != linha)
                                {
                                    pivo = matriz[linha, linha] / matriz[linha2, linha];
                                    y[linha2, 0] -= y[linha, 0] * pivo;
                                    y[linha2, 1] -= y[linha, 1] * pivo;
                                    y[linha2, 2] -= y[linha, 2] * pivo;
                                    y[linha2, 3] -= y[linha, 3] * pivo;
                                    y[linha2, 4] -= y[linha, 4] * pivo;
                                    for (coluna = 0; coluna < K; coluna++)
                                    {
                                        matriz[linha2, coluna] -= matriz[linha, coluna] * pivo;
                                    }
                                }
                            }
                        }
                        //Solução progressiva
                        temp2.definir(0, 0, 0, 0, 0);
                        for (j = 0; j < K; j++)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                a[j, k] = y[j, k] / matriz[j, j];
                                temp2.peso[k] += a[j, k] * Math.Pow(i, j);
                            }
                        }
                        pesosFiltro.Add(temp2);
                    }
                    exibirFiltroLinear = true;
                }
                ErroGolay.Content = "Concluido.";
            }*/
        }
        
        private void Ent_Data_Nascimento_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {/*
            int idade_temp = 0;
            DateTime ent = (DateTime)Ent_Data_Nascimento.SelectedDate;
            idade_temp = DateTime.Today.Year - ent.Year;
            if (ent.Month > DateTime.Today.Month) idade_temp--;
            else if (ent.Month == DateTime.Today.Month)
                if (ent.Day > DateTime.Today.Day) idade_temp--;
            Sai_Idade.Content = idade_temp.ToString();
            paciente.Nascimento = ent;*/
        }

        private void Ent_Raca_Loaded_1(object sender, RoutedEventArgs e)
        {
            //var comboBox = sender as ComboBox;
            //comboBox.ItemsSource = paciente.raca;
        }

        private void Ent_Civil_Loaded_1(object sender, RoutedEventArgs e)
        {
            //var comboBox = sender as ComboBox;
            //comboBox.ItemsSource = paciente.estadoC;
        }

        private void Ent_Educacao_Loaded_1(object sender, RoutedEventArgs e)
        {
            //var comboBox = sender as ComboBox;
            //comboBox.ItemsSource = paciente.educacao;
        }

        private void Ent_Medicamentos_TextChanged_1(object sender, TextChangedEventArgs e)
        {/*
            TextBox texto = sender as TextBox;
            int cont = 0, i = 0;
            while (i <= texto.Text.LastIndexOf(";"))
            {
                if (texto.Text[i] == ';') cont++;
                i++;
            }
            Total_Medicamentos.Content = String.Format("Total: {0} Medicamentos", cont);
            paciente.Medicamentos = i;
            paciente.Medicamentos_disc = texto.Text;*/
        }

        private void Ent_Avaliacao(object sender, RoutedEventArgs e)
        {/*
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = paciente.avaliar;*/
        }

        private void Ent_Classificacao(object sender, RoutedEventArgs e)
        {/*
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = paciente.movimento;*/
        }

        private void Ent_CF_Click(object sender, RoutedEventArgs e)
        {/*
            CheckBox temp = sender as CheckBox;
            int ind = int.Parse(temp.Name.Substring(temp.Name.LastIndexOf('_') + 1));
            paciente.CF_tabela[ind] = (bool)temp.IsChecked;
            Sai_CF_avaliar.Content = paciente.CF_avaliar();
            Sai_BOMFAQ_Pontos.Content = String.Format("Pontuação: {0}", paciente.CF_pontos.ToString());*/
        }

        private void Ent_ST_Tipo_Loaded_1(object sender, RoutedEventArgs e)
        {/*
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = paciente.ST_Tipo_Tontura;*/
        }

        private void Ent_ST_Click(object sender, RoutedEventArgs e)
        {/*
            CheckBox temp = sender as CheckBox;
            int ind = int.Parse(temp.Name.Substring(temp.Name.LastIndexOf('_') + 1));
            paciente.ST_Fatores[ind] = (bool)temp.IsChecked;*/
        }

        private void Ent_TUGT_Tempo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {/*
            if ((float)(((DecimalUpDown)sender).Value) > 0.1)
            {
                paciente.TUGT_Tempo = (float)(((DecimalUpDown)sender).Value);
                Sai_TUGT_Risco.Content = paciente.TUGT_Risco();
            }*/
        }

        private void Ent_ST_EVA_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //paciente.ST_EVA = (int)(((IntegerUpDown)sender).Value);
        }

        private void Ent_DPC_MEEM_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {/*
            IntegerUpDown temp = sender as IntegerUpDown;
            int ind = int.Parse(temp.Name.Substring(temp.Name.LastIndexOf('_') + 1));
            paciente.DPC_MEEM[ind] = (int)temp.Value;
            Sai_MEEM_Pontos.Content = paciente.MEEN_pontua().ToString() + " pontos.";*/
        }

        private void Ent_BBS_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {/*
            IntegerUpDown temp = sender as IntegerUpDown;
            int ind = int.Parse(temp.Name.Substring(temp.Name.LastIndexOf('_') + 1));
            paciente.BBS_Tabela[ind] = (int)temp.Value;
            Sai_BBS_Pontos.Content = "BBS: " + paciente.BBS_Pontos().ToString() + " pontos.";
            Sai_BBS_Categoria.Content = paciente.BBS_Categoria();*/
        }

        private void Ent_DPC_EDGA_Click(object sender, RoutedEventArgs e)
        {/*
            CheckBox temp = sender as CheckBox;
            int ind = int.Parse(temp.Name.Substring(temp.Name.LastIndexOf('_') + 1));
            paciente.DPC_EDGA[ind] = (bool)temp.IsChecked;
            Sai_GDS_Categoria.Content = paciente.GDS_categoria();
            Sai_GDS_Pontos.Content = "Pontos GDS: " + paciente.GDS_pontos.ToString();*/
        }

        private void Ent_DGI_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {/*
            IntegerUpDown temp = sender as IntegerUpDown;
            int ind = int.Parse(temp.Name.Substring(temp.Name.LastIndexOf('_') + 1));
            paciente.DGI_Tabela[ind] = (int)temp.Value;
            Sai_DGI_Categoria.Content = paciente.DGI_Categoria();
            Sai_DGI_Pontos.Content = paciente.DGI_Pontos.ToString() + " pontos.";*/
        }
        
        private void Ent_Nome_TextChanged_1(object sender, TextChangedEventArgs e) { paciente.Nome = ((TextBox)sender).Text; }
        private void Ent_Profissão_TextChanged_1(object sender, TextChangedEventArgs e) { paciente.Profissão = ((TextBox)sender).Text; }
        private void Ent_Endereco_TextChanged_1(object sender, TextChangedEventArgs e) { paciente.Endereço = ((TextBox)sender).Text; }
        private void Ent_Numero_Casa_TextChanged_1(object sender, TextChangedEventArgs e) { paciente.Numero_Casa = ((TextBox)sender).Text; }
        private void Ent_Bairro_TextChanged_1(object sender, TextChangedEventArgs e) { paciente.Bairro = ((TextBox)sender).Text; }
        private void Ent_Cidade_TextChanged_1(object sender, TextChangedEventArgs e) { paciente.Cidade = ((TextBox)sender).Text; }
        private void Ent_Telefone_TextChanged_1(object sender, TextChangedEventArgs e) { paciente.Numero_Telefone = ((TextBox)sender).Text; }
        private void Ent_Celular_TextChanged_1(object sender, TextChangedEventArgs e) { paciente.Numero_Celular = ((TextBox)sender).Text; }
        private void Ent_Protocolo_TextChanged_1(object sender, TextChangedEventArgs e) { paciente.Numero_protocolo = ((TextBox)sender).Text; }
        private void Ent_Avaliador_TextChanged_1(object sender, TextChangedEventArgs e) { paciente.Avaliador = ((TextBox)sender).Text; }
        private void Ent_Data_Avaliacao_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e) { paciente.Data_Coleta = (DateTime)((DatePicker)sender).SelectedDate; }
        private void Ent_Genero_Selected_1(object sender, RoutedEventArgs e) { paciente.Genero = ((ComboBox)sender).SelectedIndex == 0 ? true : false; }
        private void Ent_Hipoteses_Diagnósticas_TextChanged_1(object sender, TextChangedEventArgs e) { paciente.Hipoteses_diagnosticas = ((TextBox)sender).Text; }
        private void Ent_Hospitalizacao_UA_Click_1(object sender, RoutedEventArgs e) { paciente.Hospitazacao_UA = (bool)((CheckBox)sender).IsChecked; }
        private void Ent_Etilismo_Click_1(object sender, RoutedEventArgs e) { paciente.Historico_Alcolismo = (bool)((CheckBox)sender).IsChecked; }
        private void Ent_Tabagismo_Click_1(object sender, RoutedEventArgs e) { paciente.Historico_Tabagismo = (bool)((CheckBox)sender).IsChecked; }
        private void Ent_Atividade_Fisica_Click_1(object sender, RoutedEventArgs e) { paciente.Atividade_Fisica = (bool)((CheckBox)sender).IsChecked; }
        private void Ent_Cirurgia_UA_Click_1(object sender, RoutedEventArgs e) { paciente.Cirurgia_UA = (bool)((CheckBox)sender).IsChecked; }
        private void Ent_Avalia_Saude_SelectionChanged(object sender, SelectionChangedEventArgs e) { paciente.Avaliação_saude = ((ComboBox)sender).SelectedIndex; }
        private void Ent_Avalia_Visao_SelectionChanged(object sender, SelectionChangedEventArgs e) { paciente.Avaliação_visao = ((ComboBox)sender).SelectedIndex; }
        private void Ent_Avalia_Audicao_SelectionChanged(object sender, SelectionChangedEventArgs e) { paciente.Avaliação_audicao = ((ComboBox)sender).SelectedIndex; }
        private void Ent_Sente_Dor_Click(object sender, RoutedEventArgs e) { paciente.Sente_Dor = (bool)((CheckBox)sender).IsChecked; }
        private void Ent_Grau_Dor_TextChanged(object sender, TextChangedEventArgs e) { paciente.Grau_Dor = int.Parse(((TextBox)sender).Text); }
        private void Ent_Locomocao_SelectionChanged(object sender, SelectionChangedEventArgs e) { paciente.Locomocao = ((ComboBox)sender).SelectedIndex; }
        private void Ent_Transporte_SelectionChanged(object sender, SelectionChangedEventArgs e) { paciente.Transporte = ((ComboBox)sender).SelectedIndex; }
        private void Ent_Dispositivo_Auxilio_TextChanged(object sender, TextChangedEventArgs e) { paciente.Dispositivo_Auxilio = ((TextBox)sender).Text; }
        private void Ent_Quedas_Click(object sender, RoutedEventArgs e) { paciente.Quedas = (bool)((CheckBox)sender).IsChecked; }
        private void Ent_Medo_de_Cair_Click(object sender, RoutedEventArgs e) { paciente.Medo_de_Cair = (bool)((CheckBox)sender).IsChecked; }
        private void Ent_UQ_Queda_SelectionChanged(object sender, SelectionChangedEventArgs e) { paciente.UQ_Ambiente = (((ComboBox)sender).SelectedIndex == 0 ? true : false); }
        private void Ent_UQ_Ajuda_Click(object sender, RoutedEventArgs e) { paciente.UQ_Ajuda = (bool)((CheckBox)sender).IsChecked; }
        private void Ent_UQ_Lesao_Click(object sender, RoutedEventArgs e) { paciente.UQ_Lesao = (bool)((CheckBox)sender).IsChecked; }
        private void Ent_UQ_Mecanismo_TextChanged(object sender, TextChangedEventArgs e) { paciente.UQ_Mecanismo = ((TextBox)sender).Text; }
        private void Ent_UQ_Circunstancia_TextChanged(object sender, TextChangedEventArgs e) { paciente.UQ_Circunstancia = ((TextBox)sender).Text; }
        private void Ent_UQ_Outras_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e) { paciente.UQ_Outras = (int)((IntegerUpDown)sender).Value; }        
        private void Ent_ST_Tipo_SelectionChanged(object sender, SelectionChangedEventArgs e) { paciente.ST_Tipo = ((ComboBox)sender).SelectedIndex; }
        private void Ent_ST_Periodicidade_TextChanged(object sender, RoutedPropertyChangedEventArgs<object> e) { paciente.ST_Periodicidade = (int)((IntegerUpDown)sender).Value; }
        
        private void Ent_ST_Existe_Click(object sender, RoutedEventArgs e) 
        { /*
            paciente.ST_Existe = (bool)((CheckBox)sender).IsChecked;
            if (!paciente.ST_Existe)
            {
                for (int i = 0; i < 12; i++)
                {
                    var check = (CheckBox)this.FindName("Ent_ST_" + i.ToString());
                    check.IsEnabled = false;
                }
                Ent_ST_Tipo.IsEnabled = false;
                Ent_ST_Periodicidade.IsEnabled = false;
                Ent_ST_EVA.IsEnabled = false;
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    var check = (CheckBox)this.FindName("Ent_ST_" + i.ToString());
                    check.IsEnabled = true;
                }
                Ent_ST_Tipo.IsEnabled = true;
                Ent_ST_Periodicidade.IsEnabled = true;
                Ent_ST_EVA.IsEnabled = true;
            }*/
        }
        
        private void Ent_CTSIB_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            /*IntegerUpDown temp = sender as IntegerUpDown;
            int ind = int.Parse(temp.Name.Substring(temp.Name.LastIndexOf('_') + 1));
            paciente.CTSIB_Tabela[ind] = (int)temp.Value;
            switch (ind)
            {
                case 0: Sai_CTSIB_0.Content = paciente.CTSIB_Classifica(paciente.CTSIB_Tabela[ind]); break;
                case 1: Sai_CTSIB_1.Content = paciente.CTSIB_Classifica(paciente.CTSIB_Tabela[ind]); break;
                case 2: Sai_CTSIB_2.Content = paciente.CTSIB_Classifica(paciente.CTSIB_Tabela[ind]); break;
                case 3: Sai_CTSIB_3.Content = paciente.CTSIB_Classifica(paciente.CTSIB_Tabela[ind]); break;
            }*/
        }
        
        private void Ler_Pacientes()
        {/*
            paciente.ID = 0;
            if (File.Exists("../../Dados/Lista.txt"))
            {
                StreamReader arq_pacientes = new StreamReader("../../Dados/Lista.txt");
                lista_pacientes = new List<pessoa_reduzido>();
                string linha = " ";
                while (linha != "---")
                {
                    arq_pacientes.ReadLine();
                    paciente.ID++;
                }
            }
            else
            {
                File.Create("../../Dados/Lista.txt");
            }
            */
        }
        

        private void Novo_paciente_Click(object sender, RoutedEventArgs e)
        {/*
            Tab_Dados_Pessoais.Focus();
            paciente.Limpar();
            lista_pacientes.Add(paciente.resumo());      */      
        }
        private void Salvar_Paciente(object sender, RoutedEventArgs e)
        {/*
            lista_pacientes.Add(paciente.resumo());
            StreamWriter arq_pacientes = new StreamWriter("../Dados/Lista.txt");
            for (int i = 0; i < lista_pacientes.Count; i++)
                arq_pacientes.WriteLine(lista_pacientes[i].EmString());
            arq_pacientes.WriteLine("---");
            if (!nomeEditado) { completo = "../Dados/pid/" + paciente.ID + ".pac"; } 

            StreamWriter arq_completo = new StreamWriter(completo);
            arq_completo.WriteLine(paciente.ID+ " " +paciente.Nome);
            arq_completo.WriteLine(paciente.Profissão);
            arq_completo.WriteLine(paciente.Nascimento.ToString());
            arq_completo.WriteLine(paciente.Genero?"1":"0");
            arq_completo.WriteLine(paciente.Cor.ToString());
            arq_completo.WriteLine(paciente.Endereço);
            arq_completo.WriteLine(paciente.Numero_Casa);
            arq_completo.WriteLine(paciente.Cidade);
            arq_completo.WriteLine(paciente.Numero_Telefone);
            arq_completo.WriteLine(paciente.Numero_Celular);
            arq_completo.WriteLine(paciente.Numero_protocolo);
            arq_completo.WriteLine(paciente.Medico_Responsável);
            arq_completo.WriteLine(paciente.Avaliador);
            arq_completo.WriteLine(paciente.Data_Coleta.ToString());
            arq_completo.WriteLine(paciente.Estado_civil.ToString());
            arq_completo.WriteLine(paciente.Diagnostico_funcional);
            arq_completo.WriteLine(paciente.Pontos_Importantes);
            arq_completo.WriteLine(paciente.Escolaridade.ToString());
            arq_completo.WriteLine(paciente.Arranjo_Moradia);
            arq_completo.WriteLine(paciente.Hipoteses_diagnosticas);
            arq_completo.WriteLine(paciente.Medicamentos.ToString());
            arq_completo.WriteLine(paciente.Medicamentos_disc);
            arq_completo.WriteLine(paciente.Queixa_Principal);
            arq_completo.WriteLine((paciente.Hospitazacao_UA?"1":"0") + (paciente.Cirurgia_UA?"1":"0"));
            arq_completo.WriteLine((paciente.Historico_Alcolismo?"1":"0")+(paciente.Historico_Tabagismo?"1":"0")+(paciente.Atividade_Fisica?"1":"0"));
            arq_completo.WriteLine(paciente.Avaliação_saude.ToString()+paciente.Avaliação_visao.ToString()+paciente.Avaliação_audicao.ToString());
            arq_completo.WriteLine((paciente.Sente_Dor?"1":"0")+" "+ paciente.Grau_Dor.ToString());
            arq_completo.WriteLine(paciente.Locomocao.ToString()+" "+paciente.Transporte.ToString());
            arq_completo.WriteLine(paciente.Dispositivo_Auxilio);
            arq_completo.WriteLine((paciente.Quedas?"1":"0")+(paciente.Medo_de_Cair?"1":"0"));
            for (i = 0; i < 14; i++)arq_completo.Write(paciente.CF_tabela[i]?"1":"0");
            arq_completo.WriteLine((paciente.UQ_Ambiente ? "1" : "0") + (paciente.UQ_Ajuda ? "1" : "0") + (paciente.UQ_Lesao ? "1" : "0") + " " +paciente.UQ_Outras.ToString());
            arq_completo.WriteLine(paciente.UQ_Mecanismo);
            arq_completo.WriteLine(paciente.UQ_Circunstancia);
            arq_completo.WriteLine(paciente.ST_Existe ? "1" : "0" + paciente.ST_Tipo.ToString() + paciente.ST_EVA.ToString() + paciente.ST_Periodicidade.ToString());
            for (i = 0; i < 12; i++) arq_completo.Write(paciente.ST_Fatores[i] ? "1 " : "0 ");
            arq_completo.WriteLine(" ");
            for (i = 0; i < 10; i++) arq_completo.Write(paciente.DPC_MEEM[i].ToString()+" ");
            arq_completo.WriteLine(paciente.apresenta_defict ? "1" : "0");
            for (i = 0; i < 30; i++) arq_completo.Write(paciente.DPC_EDGA[i] ? "1 " : "0 ");
            arq_completo.WriteLine(" ");
            arq_completo.WriteLine(paciente.TUGT_Tempo);
            for (i = 0; i < 14; i++) arq_completo.Write(paciente.BBS_Tabela[i].ToString() + " ");
            arq_completo.WriteLine(" ");
            for (i = 0; i < 4; i++) arq_completo.Write(paciente.CTSIB_Tabela[i].ToString() + " ");
            arq_completo.WriteLine(" ");
            for (i = 0; i < 8; i++) arq_completo.Write(paciente.DGI_Tabela[i].ToString() + " ");
            arq_completo.WriteLine(" ");      */
                 
        }
        
        public void apresentar_paciente()
        {/*
            Ent_Nome.Text = paciente.Nome;
            Ent_Profissão.Text = paciente.Profissão;
            Ent_Endereço.Text = paciente.Endereço;
            Ent_Numero_Casa.Text = paciente.Numero_Casa;
            Ent_Bairro.Text = paciente.Bairro;
            Ent_Cidade.Text = paciente.Cidade;
            Ent_Numero_Telefone.Text = paciente.Numero_Telefone;
            Ent_Numero_Celular.Text = paciente.Numero_Celular;
            Ent_Protocolo.Text = paciente.Numero_protocolo;
            Ent_Avaliador.Text = paciente.Avaliador;
            Ent_Data_Avaliacao.SelectedDate = paciente.Data_Coleta;
            Ent_Data_Nascimento.SelectedDate = paciente.Nascimento;
            Ent_Genero.SelectedIndex = paciente.Genero?0:1;
            Ent_Raca.SelectedIndex = paciente.Cor;
            Ent_Civil.SelectedIndex = paciente.Estado_civil;
            Ent_Educacao.SelectedIndex = paciente.Escolaridade;
            Ent_Moradia.Text = paciente.Arranjo_Moradia;
            Ent_Hipoteses_Diagnósticas.Text = paciente.Hipoteses_diagnosticas;
            Ent_Medicamentos.Text = paciente.Medicamentos_disc;
            Ent_Hospitalizacao_UA.IsChecked = paciente.Hospitazacao_UA;
            Ent_Cirurgia_UA.IsChecked = paciente.Cirurgia_UA;
            Ent_Etilismo.IsChecked = paciente.Historico_Alcolismo;
            Ent_Tabagismo.IsChecked = paciente.Historico_Tabagismo;
            Ent_Atividade_Fisica.IsChecked = paciente.Atividade_Fisica;
            Ent_Avalia_Saude.SelectedIndex = paciente.Avaliação_saude;
            Ent_Avalia_Visão.SelectedIndex = paciente.Avaliação_visao;
            Ent_Avalia_Audicao.SelectedIndex = paciente.Avaliação_audicao;
            Ent_Sente_Dor.IsChecked =paciente.Sente_Dor;
            Ent_Grau_Dor.Text = paciente.Grau_Dor.ToString();
            Ent_Locomocao.SelectedIndex = paciente.Locomocao;
            Ent_Quedas.IsChecked = paciente.Quedas;
            Ent_Medo_de_Cair.IsChecked = paciente.Medo_de_Cair;
            for(i=0;i<14;i++){
                var check = (CheckBox)FindName("Ent_CF_"+i.ToString());
                check.IsChecked = paciente.CF_tabela[i];
            }
            Ent_UQ_Ambiente.SelectedIndex = paciente.UQ_Ambiente ? 0 : 1;
            Ent_UQ_Ajuda.IsChecked = paciente.UQ_Ajuda;
            Ent_UQ_Lesao.IsChecked = paciente.UQ_Lesao;
            Ent_UQ_Mecanismo.Text = paciente.UQ_Mecanismo;
            Ent_UQ_Circunstancia.Text = paciente.UQ_Circunstancia;
            Ent_UQ_Outras.Text = paciente.UQ_Outras.ToString();
            Ent_ST_Existe.IsChecked = paciente.ST_Existe;
            if (paciente.ST_Existe)
            {
                for (i = 0; i < 12; i++)
                {
                    var check = (CheckBox)FindName("Ent_ST_" + i.ToString());
                    check.IsEnabled = paciente.ST_Fatores[i];
                }
                Ent_ST_Tipo.SelectedIndex = paciente.ST_Tipo;
                Ent_ST_Periodicidade.Value = paciente.ST_Periodicidade;
                Ent_ST_EVA.Value = paciente.ST_EVA;
            }
            for (i = 0; i < 10; i++)
            {
                var check = (IntegerUpDown)FindName("Ent_DPC_MEEM_" + i.ToString());
                check.Value = paciente.DPC_MEEM[i];
            }
            for (i = 0; i < 30; i++)
            {
                var check = (CheckBox)FindName("Ent_DPC_EDGA_" + i.ToString());
                check.IsChecked = paciente.CF_tabela[i];
            }
            Ent_TUGT_Tempo.Value = (decimal?)paciente.TUGT_Tempo;
            for (i = 0; i < 14; i++)
            {
                var check = (IntegerUpDown)FindName("Ent_BBS_" + i.ToString());
                check.Value = paciente.BBS_Tabela[i];
            }
            for (i = 0; i < 4; i++)
            {
                var check = (IntegerUpDown)FindName("Ent_CTSIB_" + i.ToString());
                check.Value = paciente.CTSIB_Tabela[i];
            }
            for (i = 0; i < 8; i++)
            {
                var check = (IntegerUpDown)FindName("Ent_DGI_" + i.ToString());
                check.Value = paciente.DGI_Tabela[i];
            }*/

        }
        
        private void Ent_ST_Existe_Load(object sender, RoutedEventArgs e)
        {/*
            for (int i = 0; i < 12; i++)
            {
                var check = (CheckBox)FindName("Ent_ST_" + i.ToString());
                check.IsEnabled = false;
            }
            Ent_ST_Tipo.IsEnabled = false;
            Ent_ST_Periodicidade.IsEnabled = false;
            Ent_ST_EVA.IsEnabled = false;*/
        }
        
        private void Seleciona_paciente(object sender, RoutedEventArgs e)
        {
            /*StreamReader arquivo = new StreamReader(lista_pacientes[Ent_Lista_Pacientes.SelectedIndex].arquivo_completo);
            string[] linha = new string[30]; 
            linha = arquivo.ReadLine().Split();
            paciente.ID = int.Parse(linha[0]);
            paciente.Nome = linha[1];
            paciente.Profissão = arquivo.ReadLine();
            paciente.Nascimento = DateTime.Parse(arquivo.ReadLine());
            paciente.Genero = (arquivo.ReadLine()=="1");
            paciente.Cor = int.Parse(arquivo.ReadLine());
            paciente.Endereço = arquivo.ReadLine();
            paciente.Numero_Casa = arquivo.ReadLine();
            paciente.Cidade = arquivo.ReadLine();
            paciente.Numero_Telefone = arquivo.ReadLine();
            paciente.Numero_Celular = arquivo.ReadLine();
            paciente.Numero_protocolo = arquivo.ReadLine();
            paciente.Medico_Responsável = arquivo.ReadLine();
            paciente.Avaliador = arquivo.ReadLine();
            paciente.Data_Coleta = DateTime.Parse(arquivo.ReadLine());
            paciente.Estado_civil = int.Parse(arquivo.ReadLine());
            paciente.Diagnostico_funcional = arquivo.ReadLine();
            paciente.Pontos_Importantes = arquivo.ReadLine();
            paciente.Escolaridade = int.Parse(arquivo.ReadLine());
            paciente.Arranjo_Moradia = arquivo.ReadLine();
            paciente.Hipoteses_diagnosticas = arquivo.ReadLine();
            paciente.Medicamentos = int.Parse(arquivo.ReadLine());
            paciente.Medicamentos_disc = arquivo.ReadLine();
            paciente.Queixa_Principal = arquivo.ReadLine();
            linha = arquivo.ReadLine().Split();
            paciente.Hospitazacao_UA  = (linha[0]=="1");
            paciente.Cirurgia_UA = linha[1] == "1";
            linha = arquivo.ReadLine().Split();
            paciente.Historico_Alcolismo = (linha[0] == "1");
            paciente.Historico_Tabagismo = (linha[1] == "1");
            paciente.Atividade_Fisica = (linha[2] == "1");
            linha = arquivo.ReadLine().Split();
            paciente.Avaliação_saude = int.Parse(linha[0]);
            paciente.Avaliação_visao = int.Parse(linha[1]);
            paciente.Avaliação_audicao = int.Parse(linha[2]);
            linha = arquivo.ReadLine().Split();
            paciente.Sente_Dor = (linha[0]=="1");
            paciente.Grau_Dor = int.Parse(linha[1]);
            linha = arquivo.ReadLine().Split();
            paciente.Locomocao = int.Parse(linha[0]);
            paciente.Transporte = int.Parse(linha[1]);
            paciente.Dispositivo_Auxilio = arquivo.ReadLine();
            linha = arquivo.ReadLine().Split();
            paciente.Quedas  = (linha[0]== "1");
            paciente.Medo_de_Cair = (linha[1] == "1");
            linha = arquivo.ReadLine().Split();
            for (i = 0; i < 14; i++) paciente.CF_tabela[i] = (linha[i]== "1");
            linha = arquivo.ReadLine().Split();
            paciente.UQ_Ambiente = (linha[0] == "1");
            paciente.UQ_Ajuda = (linha[1] == "1");
            paciente.UQ_Lesao = (linha[2] == "1"); 
            paciente.UQ_Outras = int.Parse(linha[3]);
            paciente.UQ_Mecanismo = arquivo.ReadLine();
            paciente.UQ_Circunstancia = arquivo.ReadLine();
            linha = arquivo.ReadLine().Split();
            paciente.ST_Existe = (linha[0]== "1");
            paciente.ST_Tipo = int.Parse(linha[1]);
            paciente.ST_EVA = int.Parse(linha[2]);
            paciente.ST_Periodicidade = int.Parse(linha[3]);
            linha = arquivo.ReadLine().Split();
            for (i = 0; i < 12; i++) paciente.ST_Fatores[i] = (linha[i] == "1 ");
            linha = arquivo.ReadLine().Split();
            for (i = 0; i < 10; i++) paciente.DPC_MEEM[i] = int.Parse(linha[i]);
            paciente.apresenta_defict = (arquivo.ReadLine()=="1");
            linha = arquivo.ReadLine().Split();
            for (i = 0; i < 30; i++) paciente.DPC_EDGA[i] = (linha[i] =="1");
            paciente.TUGT_Tempo = float.Parse(arquivo.ReadLine());
            linha = arquivo.ReadLine().Split();
            for (i = 0; i < 14; i++) paciente.BBS_Tabela[i] = int.Parse(linha[i]);
            linha = arquivo.ReadLine().Split();
            for (i = 0; i < 4; i++) paciente.CTSIB_Tabela[i] = int.Parse(linha[i]);
            linha = arquivo.ReadLine().Split();
            for (i = 0; i < 8; i++) paciente.DGI_Tabela[i] = int.Parse(linha[i]);  */     
        }

        private void AplicarButter_Click_1(object sender, RoutedEventArgs e)
        {

        }

    }
}
