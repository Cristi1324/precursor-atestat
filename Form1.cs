using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsSector
{
    public partial class Form1 : Form
    {
        public bool isS;
        public bool isW;
        public bool isRight;
        public bool isLeft;
        public bool isA;
        public bool isD;
        public bool Fire;

        public int TurnSpeed = 5;
        public double ForwardAcceleration = 0.2;
        public double Acceleration = 0.1;
        public double Drag = 0.02;
        public int Enemycount;
        public int BulletSpeed=8;
        public int PlayerCooldown = 5;
        public int Score=0;

        public int PlayerMaxHealth = 10;

        public int index = 1;

        PictureBox HealthBar = new PictureBox();

        PictureBox Background = new PictureBox();

        Label HealthPercentage = new Label();

        Label ScoreLabel = new Label();

        Button Start = new Button();

        Button TryAgain = new Button();

        Button Close = new Button();

        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = false;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializePlayer();
            Background.Size = new Size(1200, 800);
            Background.BackColor = Color.Black;
            Background.Width = 1200;
            Background.Height = 800;
            CreateUI();
            Random rand = new Random();
            for(int i=1; i <=2; i++)
            {
                CreateEnemy(rand.Next(0, 1100),0, rand.Next(0, 360));
            }
        }

        void ClickedStart(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.Controls.Remove(Start);
        }
        void ClickedTryAgain(object sender, EventArgs e)
        {
            Application.Restart();
        }
        void ClickedClose(object sender, EventArgs e)
        {
            this.Close();
        }
        public void CreateUI()
        {
            Start.Top = 300;
            Start.Left = 500;
            Start.Size = new Size(150, 50);
            Start.Text = "START";
            Start.Click += ClickedStart;

            TryAgain.Top = 300;
            TryAgain.Left = 500;
            TryAgain.Size = new Size(100,40);
            TryAgain.Text = "Try Again";
            TryAgain.Click += ClickedTryAgain;

            Close.Top = TryAgain.Top + 60;
            Close.Left = TryAgain.Left;
            Close.Size = new Size(100, 40);
            Close.Text = "Close";
            Close.Click += ClickedClose;

            HealthBar.BackColor = Color.FromArgb(0, 255, 0);
            HealthBar.Width = 100;
            HealthBar.Height = 15;
            HealthBar.Left =100;
            HealthBar.Top = Background.Bottom -70;

            
            HealthPercentage.Top = HealthBar.Top;
            HealthPercentage.Left = HealthBar.Left - 100;
            HealthPercentage.Text = "Health:" + (PlayerMaxHealth / PlayerMaxHealth*100).ToString()+'%';
            HealthPercentage.Width =95;
            HealthPercentage.Font = new Font("Arial", 10, FontStyle.Bold); ;

            ScoreLabel.Top = 10;
            ScoreLabel.Left = 10;
            ScoreLabel.Font = new Font("Arial",15, FontStyle.Bold);
            ScoreLabel.Text = "Score = " + Score.ToString();
            ScoreLabel.Width = 200;

            this.Controls.Add(HealthBar);
            this.Controls.Add(HealthPercentage);
            this.Controls.Add(ScoreLabel);
            this.Controls.Add(Start);

            HealthPercentage.BringToFront();

        }

        public void InitializePlayer()
        {
            Ship Player = new Ship();
            Player.Tag = "Player";
            Player.Size = new Size(50, 50);
            Player.BackColor = Color.Black;
            Player.PositionY = 650;
            Player.PositionX = 550;
            Player.Top = (int)Player.PositionY;
            Player.Left = (int)Player.PositionX;
            Player.Health = PlayerMaxHealth;
            Player.index = 0;

            Gun PlayerGun = new Gun();
            PlayerGun.Tag = "PlayerGun";
            PlayerGun.Size = new Size(20, 20);
            PlayerGun.BackColor = Color.Blue;
            PlayerGun.PositionX = Player.PositionX + Player.Width / 2 - PlayerGun.Width / 2;
            PlayerGun.PositionY = Player.PositionY - PlayerGun.Height / 2;
            PlayerGun.Top = (int)PlayerGun.PositionY;
            PlayerGun.Left = (int)PlayerGun.PositionX;

            this.Controls.Add(PlayerGun);
            this.Controls.Add(Player);
        }

        public void CreateEnemy(int randx,int randy,int angle)
        {
            Ship Enemy = new Ship();
            Enemy.Tag = "Enemy";
            Enemy.Size = new Size(40, 40);
            Enemy.BackColor = Color.DarkRed;
            Enemy.PositionX = randx;
            Enemy.PositionY = randy;
            Enemy.Top = (int)Enemy.PositionY;
            Enemy.Left = (int)Enemy.PositionX;
            Enemy.index = index;
            Enemy.Angle = angle;
            Enemy.Health = 5;

            Gun EnemyGun = new Gun();
            EnemyGun.Tag = "EnemyGun";
            EnemyGun.Size = new Size(20, 20);
            EnemyGun.BackColor = Color.Black;
            EnemyGun.PositionX = Enemy.PositionX + Enemy.Width / 2 - EnemyGun.Width / 2;
            EnemyGun.PositionY = Enemy.PositionY - EnemyGun.Height / 2;
            EnemyGun.Top = (int)EnemyGun.PositionY;
            EnemyGun.Left = (int)EnemyGun.PositionX;
            EnemyGun.index = index;
            index++;
            Enemycount++;
            this.Controls.Add(EnemyGun);
            this.Controls.Add(Enemy);
        }

        public void CreateProjectile(int gunx,int guny,int angle,int speed,double speedx,double speedy,int index)
        {
            Projectile bullet = new Projectile();
            bullet.Tag = "bullet";
            bullet.Size = new Size(5, 5);
            bullet.BackColor = Color.Red;
            bullet.PozitionX = gunx - bullet.Width / 2;
            bullet.PozitionY = guny - bullet.Height / 2;
            bullet.Angle = angle;
            bullet.SpeedX = AngleToX(angle) * speed+speedx;
            bullet.SpeedY = AngleToY(angle) * speed+speedy;
            bullet.Left = (int)bullet.PozitionX;
            bullet.Top = (int)bullet.PozitionY;
            bullet.index = index;

            this.Controls.Add(bullet);

            bullet.BringToFront();
        }

        private double DegreesToRadians(double angle)
        {
            double rad;
            rad = angle * (Math.PI / 180);
            return rad;
        }

        private double AngleToX(double angle)
        {
            double x;
            x = Math.Sin(DegreesToRadians(angle));
            return x;
        }

        private double AngleToY(double angle)
        {
            double y;
            y = -Math.Cos(DegreesToRadians(angle));
            return y;
        }
        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                isW = true;
            }
            if (e.KeyCode == Keys.S)
            {
                isS = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                isLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                isRight = true;
            }
            if(e.KeyCode == Keys.Space)
            {
                Fire = true;
            }
            if (e.KeyCode == Keys.A)
            {
                isA = true;
            }
            if (e.KeyCode == Keys.D)
            {
                isD = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                isW = false;
            }
            if (e.KeyCode == Keys.S)
            {
                isS = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                isLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                isRight = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                Fire = false;
            }
            if (e.KeyCode == Keys.A)
            {
                isA = false;
            }
            if (e.KeyCode == Keys.D)
            {
                isD = false;
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Random rand = new Random();
            if (Enemycount < 2) CreateEnemy(rand.Next(0, 1100), 0, rand.Next(90, 180));
            foreach (Control x in this.Controls)
            {
                if (x is Ship)
                {

                    if (((Ship)x).Left < 0)
                    {
                        ((Ship)x).SpeedX = -((Ship)x).SpeedX*0.5;
                        ((Ship)x).SpeedY = ((Ship)x).SpeedY * 0.3;
                        ((Ship)x).PositionX = 0;
                    }
                    if (((Ship)x).Top < 0)
                    {
                        ((Ship)x).SpeedY = -((Ship)x).SpeedY*0.5;
                        ((Ship)x).SpeedX = ((Ship)x).SpeedX * 0.3;
                        ((Ship)x).PositionY = 0;
                    }
                    if (((Ship)x).Top > 700)
                    {
                        ((Ship)x).SpeedY = -((Ship)x).SpeedY*0.5;
                        ((Ship)x).SpeedX = ((Ship)x).SpeedX * 0.3;
                        ((Ship)x).PositionY = 700;
                    }
                    if (((Ship)x).Left > 1100)
                    {
                        ((Ship)x).SpeedX = -((Ship)x).SpeedX*0.5;
                        ((Ship)x).SpeedY = ((Ship)x).SpeedY * 0.3;
                        ((Ship)x).PositionX = 1100;
                    }

                    foreach (Control y in this.Controls)
                    {
                        if (y is Gun)
                        {
                            if (y.Tag == "PlayerGun" && x.Tag == "Player")
                            {
                                if (isRight == true)
                                {
                                    ((Ship)x).Angle = (((Ship)x).Angle + TurnSpeed) % 360;
                                }
                                if (isLeft)
                                {
                                    ((Ship)x).Angle = (((Ship)x).Angle - TurnSpeed) % 360;
                                }
                                if (isW)
                                {
                                    ((Ship)x).SpeedX += AngleToX(((Ship)x).Angle) * ForwardAcceleration;
                                    ((Ship)x).SpeedY += AngleToY(((Ship)x).Angle) * ForwardAcceleration;
                                }
                                if (isS)
                                {
                                    ((Ship)x).SpeedX -= AngleToX(((Ship)x).Angle) * Acceleration;
                                    ((Ship)x).SpeedY -= AngleToY(((Ship)x).Angle) * Acceleration;
                                }
                                if (isD)
                                {
                                    ((Ship)x).SpeedX -= AngleToX(((Ship)x).Angle-90) * Acceleration;
                                    ((Ship)x).SpeedY -= AngleToY(((Ship)x).Angle-90) * Acceleration;
                                }
                                if (isA)
                                {
                                    ((Ship)x).SpeedX -= AngleToX(((Ship)x).Angle + 90) * Acceleration;
                                    ((Ship)x).SpeedY -= AngleToY(((Ship)x).Angle + 90) * Acceleration;
                                }
                                if (Fire&&((Gun)y).Cooldown==0)
                                {
                                    CreateProjectile(
                                        ((Gun)y).Left + ((Gun)y).Width / 2,
                                        ((Gun)y).Top + ((Gun)y).Height / 2,
                                        ((Ship)x).Angle,
                                        BulletSpeed,
                                        ((Ship)x).SpeedX,
                                        ((Ship)x).SpeedY,
                                        ((Ship)x).index
                                        );
                                    (((Gun)y).Cooldown)=PlayerCooldown;
                                }else if (((Gun)y).Cooldown>0)
                                {
                                    ((Gun)y).Cooldown--;
                                }
                                ((Ship)x).PositionX += ((Ship)x).SpeedX;
                                ((Ship)x).PositionY += ((Ship)x).SpeedY;
                                ((Ship)x).SpeedX -= ((Ship)x).SpeedX * Drag;
                                ((Ship)x).SpeedY -= ((Ship)x).SpeedY * Drag;
                                ((Ship)x).Left = (int)((Ship)x).PositionX;
                                ((Ship)x).Top = (int)((Ship)x).PositionY;
                                ((Gun)y).Left = ((Ship)x).Left + ((Ship)x).Width / 2 - ((Gun)y).Width / 2 + (int)(AngleToX(((Ship)x).Angle) * 30);
                                ((Gun)y).Top = ((Ship)x).Top + ((Ship)x).Height / 2 - ((Gun)y).Height / 2 + (int)(AngleToY(((Ship)x).Angle) * 30);
                            }
                            if(x.Tag == "Enemy" && y.Tag == "EnemyGun" && ((Ship)x).index == ((Gun)y).index)
                            {
                                ((Ship)x).PositionX += ((Ship)x).SpeedX;
                                ((Ship)x).PositionY += ((Ship)x).SpeedY;
                                ((Ship)x).SpeedX -= ((Ship)x).SpeedX * Drag;
                                ((Ship)x).SpeedY -= ((Ship)x).SpeedY * Drag;
                                ((Ship)x).Left = (int)((Ship)x).PositionX;
                                ((Ship)x).Top = (int)((Ship)x).PositionY;
                                ((Gun)y).Left = (int)((Ship)x).PositionX + ((Ship)x).Width / 2 - ((Gun)y).Width / 2 + (int)(AngleToX(((Ship)x).Angle) * 30);
                                ((Gun)y).Top = (int)((Ship)x).PositionY + ((Ship)x).Height / 2 - ((Gun)y).Height / 2 + (int)(AngleToY(((Ship)x).Angle) * 30);

                                ((Ship)x).SpeedX += AngleToX(((Ship)x).Angle) * Acceleration;
                                ((Ship)x).SpeedY += AngleToY(((Ship)x).Angle) * Acceleration;

                                if (((Gun)y).Cooldown > 0)
                                {
                                    ((Gun)y).Cooldown--;
                                }
                                else
                                {
                                    CreateProjectile(
                                        ((Gun)y).Left + ((Gun)y).Width / 2,
                                        ((Gun)y).Top + ((Gun)y).Height / 2,
                                        ((Ship)x).Angle,
                                        BulletSpeed,
                                        ((Ship)x).SpeedX,
                                        ((Ship)x).SpeedY,
                                        ((Ship)x).index
                                        );
                                    ((Gun)y).Cooldown = 30;
                                }
                                ((Ship)x).Angle += 1;
                            }
                            
                        }
                    }
                }else if(x is Projectile)
                {
                    ((Projectile)x).PozitionX += ((Projectile)x).SpeedX;
                    ((Projectile)x).PozitionY += ((Projectile)x).SpeedY;
                    ((Projectile)x).Left = (int)((Projectile)x).PozitionX;
                    ((Projectile)x).Top = (int)((Projectile)x).PozitionY;
                    if (!Background.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                    }
                    foreach(Control y in this.Controls)
                    {
                        if(y is Gun)
                        {
                            foreach(Control z in this.Controls)
                            {
                                if(z is Ship)
                                {
                                    if(((Ship)z).index == ((Gun)y).index)
                                    {
                                        if(x.Bounds.IntersectsWith(z.Bounds)&& ((Projectile)x).index!= ((Ship)z).index)
                                        {
                                            this.Controls.Remove(x);
                                            ((Ship)z).Health-=1;
                                            if (((Ship)z).Health < 1){
                                                this.Controls.Remove(y);
                                                this.Controls.Remove(z);
                                                if (((Ship)z).Tag == "Player")
                                                {
                                                    timer1.Enabled = false;
                                                    this.Controls.Add(TryAgain);
                                                    this.Controls.Add(Close);
                                                }
                                                else
                                                {
                                                    Score += 10;
                                                    ScoreLabel.Text = "Score = " + Score.ToString();
                                                    Enemycount--;
                                                }
                                            }else if(((Ship)z).Tag == "Player")
                                            {
                                                HealthBar.Width = (((Ship)z).Health*100 / PlayerMaxHealth);
                                                HealthBar.BackColor = Color.FromArgb((int)(255*(double)(1- (double)((Ship)z).Health / PlayerMaxHealth)), (int)(255 * (double)((double)((Ship)z).Health / PlayerMaxHealth)), 0);
                                                HealthPercentage.Text = "Health:" + ((((Ship)z).Health*100 / PlayerMaxHealth)).ToString()+'%';
                                            }else if(((Ship)z).Tag == "Enemy")
                                            {
                                                ((Ship)z).BackColor = Color.FromArgb((int)(255 * (double)((double)((Ship)z).Health / 5)),(int)(255 * (double)(1 - (double)((Ship)z).Health / 5)),0);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
