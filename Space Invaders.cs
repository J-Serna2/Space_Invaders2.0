﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Invaders2._0
{
    public partial class Space_Invaders : Form
    {
        // instancias de las clases
        Tank loadtank;
        Invaders1 invaders1;
        Main main;
        public Space_Invaders()
        {
            InitializeComponent();

            invaders1 = new Invaders1();
            loadtank = new Tank();
            main = new Main();

            loadtank.CreateTank(this); // creo el tanque en el form
            invaders1.Create(this); // creo los aliens en el form

        }

        private int timer = 200;
        private void Space_Invaders_Load(object sender, EventArgs e)
        {

        }

        private void Timer_Main_Tick(object sender, EventArgs e)
        {
            loadtank.MovementBullet(this); // bala
            invaders1.Movement(this); // invaders

            // Balas de lso Aliens
            timer -= 3; // conteo regresivo del timer
            if (timer < 1) // condición´para ejecutar 
            {
                timer = 200; // cada que el intervalo definido en el timer del form se ejecute lanzara un bala
                invaders1.Bullet(this, "BulletAliens"); // LLamo a la bala del alien
            }

            foreach (Control x in this.Controls) // propiedades de los cuadros 
            {
                // INVASIÓN
                if (x is PictureBox && (string)x.Tag == "invaders") // asigno la x a un Picture dandole un valor de string
                                                                    // y lo comparo con el tag de los invaders
                {

                    if (x.Bounds.IntersectsWith(loadtank.tank.Bounds)) // obtengo todos los valores del tamaño de x
                                                                       // y si se cruza con los invaders
                    {
                        Timer_Main.Stop();
                        main.GameOver("Game Over **Te Han Invadido**", this); // Lanzo mensaje si se interceptan los PictureBox
                    }
                }

                // BALA TANQUE
                if (x is PictureBox && (string)x.Tag == "BulletTank") // bala del tanque
                {
                    foreach (PictureBox i in invaders1.invaders) // recorro la lista de los aliens
                    {
                        {
                            if (x.Bounds.IntersectsWith(i.Bounds)) // obtengo todos los valores del tamaño de x
                                                                                       // y si se cruza con los invaders
                            {
                                this.Controls.Remove(i); //remover el invader cuando la bala lo toca

                                main.Score += 1; // falla 
                                label2.Text = Convert.ToString(main.Score); // mostrar en el label 
                            }


                        }
                    }

                }
                //BALA INVADERS
                if (x is PictureBox && (string)x.Tag == "BulletAliens") // bala Aliens
                {
                    x.Top += 5; // muevo bala invaders
                    if (x.Bounds.IntersectsWith(loadtank.tank.Bounds)) // obtengo todos los valores del tamaño de x
                                                                       // y si se cruza con los invaders
                    {
                        this.Controls.Remove(loadtank.tank);
                        this.Controls.Remove(x);
                        Timer_Main.Stop();
                        main.GameOver("Has caído", this);
                    }
                }
            }


        }
        private void MovimientoTank(object sender, KeyEventArgs e) // Teclas tanque
        {
            int x = loadtank.tank.Location.X; // Guardo la localización

            if (x >= 920) x = 920; // limite x por derecha
            if (x <= 5) x = 5; // limite x por la izquieda
            Point point = new Point(x, 520); // Localizo el punto
            loadtank.tank.Location = point; // redibujo el picturebox

            // mover tanque
            if (e.KeyCode == Keys.Left) // muevo a la izquierda
            {
                loadtank.tank.Left -= loadtank.Speed; // resto pixeles para que se mueva a la izquierda
            }
            if (e.KeyCode == Keys.Right) // muevo a la derecha
            {
                loadtank.tank.Left += loadtank.Speed; // agrego pixeles para que se mueva a la derecha

            }
            if (e.KeyCode == Keys.A) loadtank.tank.Left -= loadtank.Speed; // muevo a la izquierda
            if (e.KeyCode == Keys.D) loadtank.tank.Left += loadtank.Speed; // muevo a la dereccha

            if (e.KeyCode == Keys.Space) // Crea bala del tanque
            {
                timer -= 40; // resta al timer
                if (timer < 20) // condición
                {
                    timer = 300; //  tiempo en que se ejcutara (milisegundos)
                    loadtank.Bullet(this, "BulletTank");
                }

            }
        }


        private void Space_Invaders_FormClosing(object sender, FormClosingEventArgs e) // Finalizar ejecución
        {
            Application.Exit(); // cerrar desde el boton de ventana
        }

        private void label2_Click(object sender, EventArgs e) // Puntuación
        {

        }
    }
}
