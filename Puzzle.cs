﻿
// Práctica realizada por:
//	José Miguel Navarro Moreno
//	José Antonio Larrubia García
//

namespace Microsoft.Samples.Kinect.ColorBasics
{
    using System;
    using System.Windows.Media;
    using Microsoft.Kinect;
    
    using System.Drawing;
    using System.Windows.Media.Imaging;
    using System.IO;
    using System.Drawing.Imaging;

    
    /// <summary>
    /// Clase que cortará una imagen y manejará los rectángulos de la imagen cortada usando RectImagen.cs.
    /// </summary>
    class Puzzle
    {
        /// <summary>
        /// La imagen que leeremos y cortaremos.
        /// </summary>
        private Image imagen;

        /// <summary>
        /// Array de imagenes donde almacenaremos cada corte de la imagen.
        /// </summary>
        private Image[] imagenes;
        
        /// <summary>
        /// Variable que determinará el orden aleatorio de las imagenes a mostrar
        /// </summary>
        private int[] pos;
        
        /// <summary>
        /// Arrays de rectángulos asociado al array de imagenes. 
        /// </summary>
        private RectImagen[] rect;
        
        /// <summary>
        /// Variable de nuestro esqueleto para poder mover el puzzle
        /// </summary>
        private Skeleton skel;
        
        /// <summary>
        /// Sensor de kinect para mover el puzzle
        /// </summary>
        private KinectSensor sensor;
        
        /// <summary>
        /// Ancho de la imagen
        /// </summary>
        private int width;
        
        /// <summary>
        /// Alto de la imagen
        /// </summary>
        private int height;
        
        /// <summary>
        /// Número de rectangulos que habrá de ancho
        /// </summary>
        private int piezas_width;
        
        /// <summary>
        ///  Número de rectangulos que habrá de alto
        /// </summary>
        private int piezas_height;
        
        /// <summary>
        ///  Tiempo que tiene el usuario para realizar el puzzle
        /// </summary>
        private int tiempo;
        
        /// <summary>
        /// Variable contadora para saber cuanto tiempo le queda al usuario para resolver el puzzle.
        /// </summary>
        private int tiempo_actual;

        /// <summary>
        ///  Variable con el tiempo de finalización para saber cuanto le queda al usuario para resolver el puzzle.
        /// </summary>
        private int tiempo_fin=-1;

        /// <summary>
        ///  Variable para saber si ha acabado el tiempo para resolver el puzzle.
        /// </summary>
        private bool fin = false;

        /// <summary>
        ///  Variable con la puntuación de las piezas que están correctamente colocadas en su sitio.
        /// </summary>
        private int puntuacion;

        /// <summary>
        /// Constructor de la clase Puzzle.
        /// </summary>
        /// <param name="im"> Nombre de la imagen que leeremos para después cortarla. </param>
        public Puzzle(String im) {
            //Comprobamos que se lee correctamente la imagen.
            try {
                imagen = Image.FromFile(im);
            }
            catch
            {
                System.Console.WriteLine("Imagen no encontrada");
            }

            //Asignamos la anchura y altura de la imagen y cuantos trozos de ella haremos.
            width=640;
            height = 480;
            piezas_width = 4;
            piezas_height = 3;
            tiempo = piezas_height * piezas_width * 8;
           
            //Declaramos imagenes,pos y rect con el tamaño total de imágenes cuando se corten. 
            pos = new int[piezas_height * piezas_width];
            rect = new RectImagen[piezas_height * piezas_width];
            imagenes = new Image[piezas_height * piezas_width];

            //Iniciamos pos a -1 para controlarlo.
            for (int i = 0; i < piezas_height*piezas_width; i++)
             {
                 pos[i] = -1;
             }

            //Y damos números aleatorios para las posiciones.
            Random a = new Random();
            for (int i = 0; i < piezas_height * piezas_width; i++)
            {
                int n = a.Next(0, piezas_height * piezas_width);

                while (pos[n] != -1) {
                    n=(n+1)% (piezas_height * piezas_width);
                }

                pos[n] = i;
            }

            //En el siguiente bloque cortamos las imagenes las asignamos en las posiciones aleatorias y las convertimos a algo que podemos manejas, 
            //por último asignamos la imagen a un rectángulo del array de rectángulos.
            //Esta parte del código la sacamos de stackoverflow, tanto la parte de cortar las imagenes como la de transformarlas en algo que podemos mostrar.
            for(int i = 0; i < piezas_height * piezas_width; i++) {

                //Codigo para cortar las imagenes sacado y adaptado de:
                /////////////////////////////////////////////////////////////////////////////////////
                //http://stackoverflow.com/questions/13625891/cut-an-image-into-9-pieces-c-sharp
                /////////////////////////////////////////////////////////////////////////////////////

                imagenes[i] = new Bitmap(imagen.Size.Width/ piezas_width, imagen.Size.Height/ piezas_height);
                var gra = Graphics.FromImage(imagenes[i]);
                int ipos = i % piezas_width;
                int jpos = i/ piezas_width;
                gra.DrawImage(imagen, new Rectangle(0, 0, imagen.Size.Width / piezas_width, imagen.Size.Height / piezas_height), 
                    new Rectangle(ipos* imagen.Size.Width /piezas_width, jpos* imagen.Size.Height / piezas_height, imagen.Size.Width / piezas_width, imagen.Size.Height / piezas_height),
                    GraphicsUnit.Pixel);
                gra.Dispose();

                //Código para transformar las imágenes a BitmapImage sacado de:
                /////////////////////////////////////////////////////////////////////////////////////
                // http://joe-bq-wang.iteye.com/blog/1685024
                /////////////////////////////////////////////////////////////////////////////////////
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                MemoryStream ms = new MemoryStream();
                imagenes[i].Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                
                //Asignamos a cada rectángulo la imagen transformada en la posición aleatoria.
                int i_rec = pos[i] % piezas_width;
                int j_rec = pos[i] / piezas_width;

                rect[i] = new RectImagen(i_rec * width / piezas_width, j_rec * height / piezas_height, width / piezas_width, height / piezas_height, bi);
                rect[i].setPosicionOld(rect[i].getPosicion());

            }
        }
        
        /// <summary>
        /// Constructor de la clase puzzle, la diferencia con el anterior es que elegiremos cuantas filas y columnas queremos, el resto es igual que el constructor anterior por lo que no necesita comentarios extra.
        /// </summary>
        /// <param name="im"> Nombre de la imagen que leeremos para después cortarla </param>
        /// <param name="w"> Ancho que tendrá la imagen total en pantalla </param>
        /// <param name="h"> Alto que tendrá la imagen total en pantalla </param>
        /// <param name="p_w"> Número de rectángulos horizontales de la imagen </param>
        /// <param name="p_h"> Número de rectángulos verticales de la imagen </param>
        public Puzzle(String im,int w,int h,int p_w,int p_h)
        {
            try
            {
                imagen = Image.FromFile(im);
            }
            catch
            {
                System.Console.WriteLine("Imagen no encontrada");
            }

            width = w;
            height = h;
            piezas_width = p_w;
            piezas_height = p_h;
            tiempo = piezas_height * piezas_width * 10;

            pos = new int[piezas_height * piezas_width];
            rect = new RectImagen[piezas_height * piezas_width];
            imagenes = new Image[piezas_height * piezas_width];

            for (int i = 0; i < piezas_height * piezas_width; i++) 
            { 
                pos[i] = -1; 
            }

            Random a = new Random();
            for (int i = 0; i < piezas_height * piezas_width; i++)
            {
                int n = a.Next(0, piezas_height * piezas_width);

                while (pos[n] != -1)
                {
                    n = (n + 1) % (piezas_height * piezas_width);
                }

                pos[n] = i;
            }

            for (int i = 0; i < piezas_height * piezas_width; i++)
            {
                imagenes[i] = new Bitmap(imagen.Size.Width / piezas_width, imagen.Size.Height / piezas_height);
                var gra = Graphics.FromImage(imagenes[i]);
                int ipos = i % piezas_width;
                int jpos = i / piezas_width;
                gra.DrawImage(imagen, new Rectangle(0, 0, imagen.Size.Width / piezas_width, imagen.Size.Height / piezas_height),
                    new Rectangle(ipos * imagen.Size.Width / piezas_width, jpos * imagen.Size.Height / piezas_height, imagen.Size.Width / piezas_width, imagen.Size.Height / piezas_height),
                    GraphicsUnit.Pixel);
                gra.Dispose();

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                MemoryStream ms = new MemoryStream();
                imagenes[i].Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();

                int i_rec = pos[i] % piezas_width;
                int j_rec = pos[i] / piezas_width;

                rect[i] = new RectImagen(i_rec * width / piezas_width, j_rec * height / piezas_height, width / piezas_width, height / piezas_height, bi);
                rect[i].setPosicionOld(rect[i].getPosicion());

            }
        }
        
        /// <summary>
        /// Función para actualizar el esqueleto.
        /// </summary>
        /// <param name="sk"> Nuevo esqueleto que asignaremos </param>
        public void actualizarSkeleto(Skeleton sk) { 
            skel = sk; 
        }
        
        /// <summary>
        /// Función para asignar un sensor.
        /// </summary>
        /// <param name="s"> Sensor de kienct que asignaremos </param>
        public void asignarSensor(KinectSensor s) { 
            sensor = s; 
        }

        /// <summary>
        /// Función para iniciar el tiempo que tendrá el usuario para resolver el puzzle.
        /// </summary>
        public void iniciarTiempo() {
            tiempo_actual = DateTime.Now.Day*24*3600 + DateTime.Now.Hour*3600 + DateTime.Now.Minute*60 + DateTime.Now.Second;
            tiempo_fin = tiempo_actual + tiempo;
        }

        /// <summary>
        /// Función para actualizar el tiempo actual.
        /// </summary>
        public void actualizarTiempo() {
            tiempo_actual = DateTime.Now.Day * 24 * 3600 + DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second;

        }

        /// <summary>
        /// Función para obtener el tiempo que le queda al usuario, si llega a 0 se quedará en 0.
        /// </summary>
        public int getTiempo() {
            if (tiempo_fin - tiempo_actual > 0)
                return tiempo_fin - tiempo_actual;
            else
                return 0;
        }

        /// <summary>
        /// Función para obtener si ha acabado o no el tiempo.
        /// </summary>
        public bool getFin() { 
            return fin; 
        }

        /// <summary>
        /// Función para obtener la puntuación.
        /// </summary>
        public int getPuntuacion() { 
            return puntuacion; 
        }
               
        /// <summary>
        /// Función para dibujar el puzzle inicial y las piezas del puzzle que no estén cogidas.
        /// </summary>
        /// <param name="dc"> Parámetro necesario para dibujar </param>
        public void DrawPuzzle(DrawingContext dc)
        {
            for(int i = 0; i < piezas_height * piezas_width; i++)
            {
                //Llamamos a la función dibujar de todos los rectángulos que no estén cogidos.
                if(!rect[i].getCogido())
                    rect[i].dibujar(dc);
            }

            //Actualizamos el tiempo.
            actualizarTiempo();

            //Comprobamos si el tiempo actual ha sobrepasado el final o si ha empezado la ejecución para resolver el puzzle.
            if (tiempo_actual > tiempo_fin&& tiempo_fin!=-1)
            {
                //Ponemos la variable que indica si ha terminado a true.
                fin = true;
                
                //Calculamos la puntuación que ha obtenido el usuario.
                puntuacion = 0;
                for(int i = 0; i < piezas_height * piezas_width; i++)
                {
                    if (pos[i] == i)
                        puntuacion += 1;
                }
                puntuacion =(int)((puntuacion * 100.0)/((piezas_height* piezas_width*1.0)));
            }
        }
        
        /// <summary>
        /// Función para dibujar las piezas del puzzle que estén cogidas
        /// </summary>
        /// <param name="dc"> Parámetro necesario para dibujar </param>
        public void DrawPuzzleCogidos( DrawingContext dc)
        {
            for (int i = 0; i < piezas_height * piezas_width; i++)
            {
                if (rect[i].getCogido())
                {
                    rect[i].dibujar(dc, SkeletonPointToScreen(skel.Joints[JointType.HandRight].Position), SkeletonPointToScreen(skel.Joints[JointType.HandLeft].Position));
                }
            }
            
        }
        
        /// <summary>
        /// Función que para coger una pieza del puzzle.
        /// </summary>
        /// <param name="mano"> Mano con la que se cogerá la pieza. </param>
        public void coger(String mano) {
            if (getTiempo() > 0)
            {
                for (int i = 0; i < piezas_height * piezas_width; i++)
                {
                    rect[i].coger(mano, SkeletonPointToScreen(skel.Joints[JointType.HandRight].Position), SkeletonPointToScreen(skel.Joints[JointType.HandLeft].Position));
                }
            }
        }
        
        /// <summary>
        /// Función para soltar una pieza del puzzle.
        /// </summary>
        /// <param name="mano"> Mano con la que se suelta la pieza. </param>
        public void soltar(String mano)
        {
            for (int i = 0; i < piezas_height * piezas_width; i++)
            {
                //Primero comprobamos que esté cogida una pieza.
                //Comprobamos que la mano con la que vamos a soltar la pieza es la mano con la que está cogida
                if (rect[i].getCogido())
                    if (rect[i].getMano() == mano) {
                        if (mano == "left") {
                            //Si es la mano izquierda pasamos la posicion de la mano izquierda a un punto
                            Point p = SkeletonPointToScreen(skel.Joints[JointType.HandLeft].Position);

                            int rec_inter = -1;
                            for (int j = 0; j < piezas_height * piezas_width; j++)
                            {
                                //Buscamos cual es la pieza que está cogida y probamos si está en la posición de otro rectángulo
                                if (!rect[j].getCogido() && rect[j].inRectImagen(p.X, p.Y))
                                {
                                    rec_inter = j;
                                }
                            }
  
                            //Si está en la posición de otra pieza al soltar la pieza.
                            if (rec_inter != -1 && i!= rec_inter)
                            {
                                //Intercambiamos la posición de las piezas.
                                Point p_nuevo = rect[rec_inter].getPosicion();
                                rect[rec_inter].setPosicion(rect[i].getPosicionOld());
                                rect[i].soltar(mano);
                                rect[i].setPosicion(rect[rec_inter].getPosicionOld());
                                rect[rec_inter].setPosicionOld(rect[rec_inter].getPosicion());
                                rect[i].setPosicionOld(rect[i].getPosicion());
                                
                                //E intercambiamos la posición dentro del array de posiciones para luego calcular la puntuación de los que estén bien colocados.
                                int a = pos[i];
                                pos[i] = pos[rec_inter];
                                pos[rec_inter] = a;
                            }
                            //Si no la soltamos donde estaba originalmente
                            else 
                                rect[i].soltar(mano);
                        }
                        //Se hace lo mismo con la mano derecha pero con el punto de la misma.
                        else if(mano=="right"){
                            Point p = SkeletonPointToScreen(skel.Joints[JointType.HandRight].Position);
                            int rec_inter = -1;

                            for (int j = 0; j < piezas_height * piezas_width; j++)
                            {
                                if (!rect[j].getCogido() && rect[j].inRectImagen(p.X, p.Y))
                                {
                                    rec_inter = j;
                                }
                            }

                            if (rec_inter != -1 && i!=rec_inter)
                            {
                                Point p_nuevo = rect[rec_inter].getPosicion();
                                rect[rec_inter].setPosicion(rect[i].getPosicionOld());
                                rect[i].soltar(mano);
                                rect[i].setPosicion(rect[rec_inter].getPosicionOld());
                                rect[rec_inter].setPosicionOld(rect[rec_inter].getPosicion());
                                rect[i].setPosicionOld(rect[i].getPosicion());
                                
                                int a = pos[i];
                                pos[i] = pos[rec_inter];
                                pos[rec_inter] = a;
                            }
                            else 
                                rect[i].soltar(mano);
                        }
                    }
             }
        }

        /// <summary>
        /// Maps a SkeletonPoint to lie within our render space and converts to Point
        /// </summary>
        /// <param name="skelpoint">point to map</param>
        /// <returns>mapped point</returns>
        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }
    }
}
