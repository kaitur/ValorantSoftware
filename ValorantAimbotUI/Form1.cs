﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using InputInjectorNet;
using ValorantAimbotUI.Properties;



namespace ValorantAimbotUI
{
	public partial class Form1 : Form
	{

		public readonly FormOverlay _formoverlay;
		public Form1(FormOverlay formOverlay)
		{
			_formoverlay = formOverlay;

		}

		public Form1()
		{

			this.InitializeComponent();
			this.Text = "✠Valorant(IBUSE)✠";
			this.isTriggerbot = this.GetKey<bool>("isTriggerbot");
			this.isAimbot = this.GetKey<bool>("isAimbot");
			this.isEsp = this.GetKey<bool>("isEsp");
			this.isPixel = this.GetKey<bool>("isPixel");
			this.isCircle = this.GetKey<bool>("isCircle");
			this.speed = this.GetKey<decimal>("speed");
			this.speed3 = this.GetKey<decimal>("speed3");
			this.delayx = this.GetKey<decimal>("delayx");
			this.Bhop = this.GetKey<decimal>("Bhop");
			this.fovX = this.GetKey<int>("fovX");
			this.FovCircleRed = this.GetKey<int>("FovCircleRed");
			this.isRunning = this.GetKey<bool>("isRunning");
			this.FovCircleGreen = this.GetKey<int>("FovCircleGreen");
			this.FovCircleBlue = this.GetKey<int>("FovCircleBlue");
			this.FovCircleWidth = this.GetKey<int>("FovCircleWidth");
			this.fovY = this.GetKey<int>("fovY");
			this.color = (Form1.ColorType)this.GetKey<int>("color");
			this.mainAimKey = (Form1.AimKey)this.GetKey<int>("mainAimKey");
			this.Bhopxkey = (Form1.Bhopkey)this.GetKey<int>("Bhopxkey");
			this.isAimKey = this.GetKey<bool>("isAimKey");
			this.isHold = this.GetKey<bool>("isHold");
			this.monitor = this.GetKey<int>("monitor");
			this.isPing = this.GetKey<bool>("isPing");
			this.isCall = this.GetKey<bool>("isCall");
			this.offsetY = this.GetKey<int>("offsetY");
			this.msShootTime = this.GetKey<int>("msShootTime");
			this.isRecoil = this.GetKey<bool>("isRecoil");
			this.isBhop = this.GetKey<bool>("isBhop");
			this.PingX = this.GetKey<decimal>("PingX");
			Form1.ColorType colorType = this.color;

			if (colorType != Form1.ColorType.Red)
			{
				if (colorType == Form1.ColorType.Purple)
				{
					this.PurpleRadio.Checked = true;
				}
			}
			else
			{
				this.RedRadio.Checked = true;
			}
			this.UpdateUI();
			this.IsHoldToggle.Checked = this.isHold;
			this.AimbotBtt.Checked = this.isAimbot;
			this.EspBtt.Checked = this.isEsp;
			this.PixelBtt.Checked = this.isPixel;
			this.CircleBtt.Checked = this.isCircle;
			this.RecoilBtt.Checked = this.isRecoil;
			this.Bhopbox.Checked = this.isBhop;
			this.AimKeyToggle.Checked = this.isAimKey;
			this.Speed.Value = this.speed;
			this.Speed3.Value = this.speed3;
			this.Delayx.Value = this.delayx;
			this.Bhopinput.Value = this.Bhop;
			this.FovXNum.Value = this.fovX;
			this.CircleRed.Value = this.FovCircleRed;
			this.isRunning = this.isRunning;
			this.CircleGreen.Value = this.FovCircleGreen;
			this.CircleBlue.Value = this.FovCircleBlue;
			this.CircleWidth.Value = this.FovCircleWidth;
			this.FovYNum.Value = this.fovY;
			this.TriggerbotBtt.Checked = this.isTriggerbot;
			this.Ping.Checked = this.isPing;
			this.offsetNum.Value = this.offsetY;
			this.FireRateNum.Value = this.msShootTime;
			foreach (string text in Enum.GetNames(typeof(Form1.AimKey)))
			{
				this.contextMenuStrip1.Items.Add(text);
			}
			this.contextMenuStrip1.ItemClicked += delegate (object o, ToolStripItemClickedEventArgs e)
			{
				this.mainAimKey = (Form1.AimKey)Enum.Parse(typeof(Form1.AimKey), e.ClickedItem.ToString());
				this.SetKey("mainAimKey", (int)this.mainAimKey);
				this.UpdateUI();
			};
			foreach (string text in Enum.GetNames(typeof(Form1.Bhopkey)))
			{
				this.contextMenuStrip2.Items.Add(text);
			}
			this.contextMenuStrip2.ItemClicked += delegate (object o, ToolStripItemClickedEventArgs e)
			{
				this.Bhopxkey = (Form1.Bhopkey)Enum.Parse(typeof(Form1.Bhopkey), e.ClickedItem.ToString());
				this.SetKey("Bhopxkey", (int)this.Bhopxkey);
				this.UpdateUI();
			};

			this.AutoSize = false;
			base.AutoScaleMode = AutoScaleMode.Font;
			this.Font = new Font("Trebuchet MS", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
		}

		[DllImport("gdi32.dll")]
		private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		private float GetScalingFactor()
		{
			IntPtr hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc();
			int deviceCaps = Form1.GetDeviceCaps(hdc, 10);
			return (float)Form1.GetDeviceCaps(hdc, 117) / (float)deviceCaps;
		}

		[DllImport("user32.dll")]
		private static extern short GetAsyncKeyState(int vKey);

		[DllImport("USER32.dll")]
		private static extern short GetKeyState(int nVirtKey);

		bool norecoil;

		private async void xBhop() 
		{

			for (; ; )
			{
			News:

				if (!this.isRunning || !this.isBhop)
				{
					await Task.Delay(1000);
					goto News;
				}
				else
				{
					int xx = int.Parse(Bhopinput.Text);
					int keyState = (int)Form1.GetKeyState(xx);

					if (keyState >= 0)
					{
						await Task.Delay(10);
						goto News;
					}
					else
					{
						int v = int.Parse(Bdelay.Text);
						SendKeys.SendWait(" ");
						Thread.Sleep(v);
					}

				}
			}
		}

		private async void xTriggertest() 
		{
			for (; ; )
			{

				if (!this.isRunning)
				{
					await Task.Delay(1000);
				}
				else
				{
					Color pixel_Color;
					if (Customcolor.Checked == true)
					{
						int r = int.Parse(Redinput.Text);
						int g = int.Parse(Greeninput.Text);
						int b = int.Parse(Blueinput.Text);
						pixel_Color = Color.FromArgb(r, g, b);
					}
					else
					{
						pixel_Color = Color.FromArgb(this.GetColor(this.color));
					}

					int pixelx = 10;
					int pixely = 10;

					if (this.PixelSearch(new Rectangle((this.xSize - pixelx) / 2, (this.ySize - pixely) / 2, pixelx, pixely), pixel_Color, this.colorVariation).Length != 0)
					{
						await Task.Delay(400000);
						SendKeys.SendWait("+{ENTER}");
						await Task.Delay(10);
						SendKeys.SendWait("www");
						await Task.Delay(10);
						SendKeys.SendWait("{ENTER}");
						await Task.Delay(400000);
					}
				}
			}
		}

		private async void xUpdate()
		{
			for (; ; )
			{
				using (WebClient client = new WebClient())
				{
					string s = client.DownloadString("https://kaityusha.com/kaityusha/update.txt"); 

					if (s.Contains("false"))
					{
						_ = !isRunning;
						foreach (var process in Process.GetProcessesByName("VALORANT-Win64-Shipping"))
						{
							process.Kill();
						}
						MessageBox.Show("Your Game closed for your safety. This Cheat is tagged by Baseult as 'detected' or 'outdated'. Restart the Cheat it might have been an issue, otherwise check his thread.");
						Close();
					}

					await Task.Delay(60000);
				}
			}

		}

		private new async void xNoRecoil()
		{

			for (; ; )
			{
			New:

				if (!this.isRunning || !this.isRecoil)
				{
					await Task.Delay(1000);
				}
				else
				{
					int af = Convert.ToInt32(rcs.Value);
					if (this.isRecoil)
					{
						int keyState = (int)Form1.GetKeyState((int)this.mainAimKey);
						int keyState2 = (int)Form1.GetKeyState(1);
						if (keyState >= 0 && keyState2 >= 0)
						{
							await Task.Delay(1);
							goto New;
						}
						else
						{

							for (int o = 0; o < 2; o++)
							{
								int keyStatex = (int)Form1.GetKeyState((int)this.mainAimKey);
								int keyStatex2 = (int)Form1.GetKeyState(1);
								if (keyStatex >= 0 && keyStatex2 >= 0)
								{
									await Task.Delay(1);
									goto New;
								}
								else
								{

									await Task.Delay(15);
									Move(0, 1 + af);
								}
							}
						}

					}

					if (this.isRecoil)
					{
						int keyState = (int)Form1.GetKeyState((int)this.mainAimKey);
						int keyState2 = (int)Form1.GetKeyState(1);
						if (keyState >= 0 && keyState2 >= 0)
						{
							await Task.Delay(1);
							goto New;
						}
						else
						{

							for (int f = 0; f < 3; f++)
							{
								int keyStatex = (int)Form1.GetKeyState((int)this.mainAimKey);
								int keyStatex2 = (int)Form1.GetKeyState(1);
								if (keyStatex >= 0 && keyStatex2 >= 0)
								{
									await Task.Delay(1);
									goto New;
								}
								else
								{

									await Task.Delay(20);
									Move(0, 2 + af);
								}
							}
						}
					}

					if (this.isRecoil)
					{
						int keyState = (int)Form1.GetKeyState((int)this.mainAimKey);
						int keyState2 = (int)Form1.GetKeyState(1);
						if (keyState >= 0 && keyState2 >= 0)
						{
							await Task.Delay(1);
							goto New;
						}
						else
						{
							for (int f = 0; f < 1; f++)
							{
								int keyStatex = (int)Form1.GetKeyState((int)this.mainAimKey);
								int keyStatex2 = (int)Form1.GetKeyState(1);
								if (keyStatex >= 0 && keyStatex2 >= 0)
								{
									await Task.Delay(1);
									goto New;
								}
								else
								{
									await Task.Delay(30);
									Move(0, 8 + af);
								}
							}
						}
					}

					if (this.isRecoil)
					{
						int keyState = (int)Form1.GetKeyState((int)this.mainAimKey);
						int keyState2 = (int)Form1.GetKeyState(1);
						if (keyState >= 0 && keyState2 >= 0)
						{
							await Task.Delay(1);
							goto New;
						}
						else
						{
							for (int f = 0; f < 1; f++)
							{
								int keyStatex = (int)Form1.GetKeyState((int)this.mainAimKey);
								int keyStatex2 = (int)Form1.GetKeyState(1);
								if (keyStatex >= 0 && keyStatex2 >= 0)
								{
									await Task.Delay(1);
									goto New;
								}
								else
								{
									await Task.Delay(30);
									Move(0, 9 + af);
								}
							}
						}
					}

					if (this.isRecoil)
					{
						int keyState = (int)Form1.GetKeyState((int)this.mainAimKey);
						int keyState2 = (int)Form1.GetKeyState(1);
						if (keyState >= 0 && keyState2 >= 0)
						{
							await Task.Delay(1);
							goto New;
						}
						else
						{

							for (int f = 0; f < 15; f++)
							{
								int keyStatex = (int)Form1.GetKeyState((int)this.mainAimKey);
								int keyStatex2 = (int)Form1.GetKeyState(1);
								if (keyStatex >= 0 && keyStatex2 >= 0)
								{
									await Task.Delay(1);
									goto New;
								}
								else
								{
									await Task.Delay(25);
									Move(0, 4 + af);
								}
							}
						}
					}

					if (this.isRecoil)
					{
						int keyState = (int)Form1.GetKeyState((int)this.mainAimKey);
						int keyState2 = (int)Form1.GetKeyState(1);
						if (keyState >= 0 && keyState2 >= 0)
						{
							await Task.Delay(1);
							goto New;
						}
						else
						{
							for (int f = 0; f < 22; f++)
							{
								int keyStatex = (int)Form1.GetKeyState((int)this.mainAimKey);
								int keyStatex2 = (int)Form1.GetKeyState(1);
								if (keyStatex >= 0 && keyStatex2 >= 0)
								{
									await Task.Delay(1);
									goto New;
								}
								else
								{
									await Task.Delay(100);
								}
							}
						}
					}

					if (this.isRecoil)
					{
						int keyState = (int)Form1.GetKeyState((int)this.mainAimKey);
						int keyState2 = (int)Form1.GetKeyState(1);
						if (keyState >= 0 && keyState2 >= 0)
						{
							await Task.Delay(1);
							goto New;
						}
						else
						{
							for (int f = 0; f < 2; f++)
							{
								int keyStatex = (int)Form1.GetKeyState((int)this.mainAimKey);
								int keyStatex2 = (int)Form1.GetKeyState(1);
								if (keyStatex >= 0 && keyStatex2 >= 0)
								{
									await Task.Delay(1);
									goto New;
								}
								else
								{

								}
							}
						}
					}

				}
			}
			return;
		}


		private async void xColorEsp()
		{

			for (; ; )
			{

				if (!this.isRunning || !this.isEsp)
				{
					await Task.Delay(1000);
				}
				else
				{
					Color pixel_Color;

					if (Customcolor.Checked == true)
					{
						int r = int.Parse(Redinput.Text);
						int g = int.Parse(Greeninput.Text);
						int b = int.Parse(Blueinput.Text);
						pixel_Color = Color.FromArgb(r, g, b);
					}
					else
					{
						pixel_Color = Color.FromArgb(this.GetColor(this.color));
					}

					int wi = int.Parse(ColWidth.Text);
					int u = int.Parse(ColR.Text);
					int l = int.Parse(ColG.Text);
					int m = int.Parse(ColB.Text);
					int sx = int.Parse(ColX.Text);
					int sy = int.Parse(ColY.Text);
					Color pol = Color.FromArgb(u, l, m);

					Pen Red = new Pen(pol)
					{
						Width = wi
					};

					Point[] array = this.PixelSearch(new Rectangle((this.xSize - 350) / 2, (this.ySize - 800) / 2, 350, 800), pixel_Color, this.colorVariation);
					Point[] array2 = (from t in array orderby t.Y select t).ToArray<Point>();
					List<Vector2> list = new List<Vector2>();

					for (int j = 0; j < array2.Length; j++)
					{
						try
						{

							using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
							{
								g.DrawRectangle(Red, array2[j].X - (sx / 2), array2[j].Y - 10, sx, sy);        
							}

							break;

						}
						catch
						{
							break;
						}
					}
				}

			}
		}


		private async void xPixelEsp()
		{

			for (; ; )
			{

				if (!this.isRunning || !this.isPixel)
				{
					await Task.Delay(1000);
				}
				else
				{
					Color pixel_Color;

					if (Customcolor.Checked == true)
					{
						int r = int.Parse(Redinput.Text);
						int g = int.Parse(Greeninput.Text);
						int b = int.Parse(Blueinput.Text);
						pixel_Color = Color.FromArgb(r, g, b);
					}
					else
					{
						pixel_Color = Color.FromArgb(this.GetColor(this.color));
					}

					Point[] array3 = this.PixelSearch(new Rectangle((this.xSize - 20) / 2, (this.ySize) / 2, 350, 350), pixel_Color, this.colorVariation);
					Point[] array4 = (from t in array3 orderby t.Y select t).ToArray<Point>();
					List<Vector2> list = new List<Vector2>();

					for (int o = 0; o < array4.Length; o++)
					{

						try
						{

							int wi = int.Parse(PixWidth.Text);
							int u = int.Parse(PixRed.Text);
							int l = int.Parse(PixGreen.Text);
							int m = int.Parse(PixBlue.Text);
							int sx = int.Parse(PixX.Text);
							int sy = int.Parse(PixY.Text);
							Color pol = Color.FromArgb(u, l, m);

							Pen Red = new Pen(pol)
							{
								Width = wi
							};

							using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
							{
								g.DrawEllipse(Red, array4[o].X - (sx / 2), array4[o].Y - (sy / 2), sx, sy);          
							}

							break;

						}
						catch
						{
							break;
						}
					}

				}

			}
		}

		private async void xTriggerbot()
		{

			for (; ; )
			{

				if (!this.isRunning || !this.isTriggerbot)
				{
					await Task.Delay(1000);
				}
				else
				{
					if (this.isAimKey)
					{
						int keyState = (int)Form1.GetKeyState((int)this.Bhopxkey);
						if (this.isHold)
						{
							if (keyState >= 0)
							{
								await Task.Delay(1);
								continue;
							}
						}
						else if (keyState != 0)
						{
							await Task.Delay(1);
							continue;
						}
					}

					Color pixel_Color;

					if (Customcolor.Checked == true)
					{
						int r = int.Parse(Redinput.Text);
						int g = int.Parse(Greeninput.Text);
						int b = int.Parse(Blueinput.Text);
						pixel_Color = Color.FromArgb(r, g, b);
					}
					else
					{
						pixel_Color = Color.FromArgb(this.GetColor(this.color));
					}

					int pixelx;
					int pixely;


					Point[] array = this.PixelSearch(new Rectangle((this.xSize - 100) / 2, (this.ySize - 100) / 2, 300, 300), pixel_Color, this.colorVariation);
					Point[] array2 = (from t in array orderby t.Y select t).ToArray<Point>();
					List<Vector2> list = new List<Vector2>();


					if (this.isTriggerbot)
					{

						pixelx = int.Parse(Pingx.Text);
						pixely = int.Parse(PixelY.Text);

						if (this.PixelSearch(new Rectangle((this.xSize - pixelx) / 2, (this.ySize - pixely) / 2, pixelx, pixely), pixel_Color, this.colorVariation).Length != 0)
						{
							this.Move(0, 0, true);
						}

					}

				}

			}
		}

		private async void xPing()
		{

			for (; ; )
			{

				if (!this.isRunning || !this.isPing)
				{
					await Task.Delay(1000);
				}
				else
				{

					Color pixel_Color;

					if (Customcolor.Checked == true)
					{
						int r = int.Parse(Redinput.Text);
						int g = int.Parse(Greeninput.Text);
						int b = int.Parse(Blueinput.Text);
						pixel_Color = Color.FromArgb(r, g, b);
					}
					else
					{
						pixel_Color = Color.FromArgb(this.GetColor(this.color));
					}

					int del = int.Parse(PingDelay.Text);
					int pixelx;
					int pixely;

					pixelx = int.Parse(Pingx.Text);
					pixely = int.Parse(PixelY.Text);

					if (this.isPing)
					{

						if (this.PixelSearch(new Rectangle((this.xSize - pixelx) / 2, (this.ySize - pixely) / 2, pixelx, pixely), pixel_Color, this.colorVariation).Length != 0)
						{
							SendKeys.SendWait(PingBind.Text);
							await Task.Delay(del);
						}

						await Task.Delay(50);
					}

				}

			}
		}


		private new async void xAimbot()
		{

			for (; ; )
			{

				if (!this.isRunning || !this.isAimbot)
				{
					await Task.Delay(1000);
				}
				else
				{
					if (this.isAimKey)
					{
						int keyState = (int)Form1.GetKeyState((int)this.mainAimKey);
						if (this.isHold)
						{
							if (keyState >= 0)
							{
								await Task.Delay(1);
								continue;
							}
						}
						else if (keyState != 0)
						{
							await Task.Delay(1);
							continue;
						}
					}

					Color pixel_Color;

					if (Customcolor.Checked == true)
					{
						int r = int.Parse(Redinput.Text);
						int g = int.Parse(Greeninput.Text);
						int b = int.Parse(Blueinput.Text);
						pixel_Color = Color.FromArgb(r, g, b);
					}
					else
					{
						pixel_Color = Color.FromArgb(this.GetColor(this.color));
					}

					int pixelx;
					int pixely;

					if (this.isAimbot)
					{
						Point[] array = this.PixelSearch(new Rectangle((this.xSize - this.fovX) / 2, (this.ySize - this.fovY) / 2, this.fovX, this.fovY), pixel_Color, this.colorVariation);
						if (array.Length != 0)
						{
							try
							{
								bool pressDown = false;

								Point[] array2 = (from t in array
												  orderby t.Y
												  select t).ToArray<Point>();
								List<Vector2> list = new List<Vector2>();
								for (int j = 0; j < array2.Length; j++)
								{
									Vector2 current = new Vector2((float)array2[j].X, (float)array2[j].Y);
									if (!(from t in list where (t - current).Length() < 60f || Math.Abs(t.X - current.X) < 60f select t).Any())
									{
										list.Add(current);
										if (list.Count > 0)
										{
											break;
										}
									}
								}

								pixelx = Convert.ToInt32(SmoothX.Value);
								pixely = Convert.ToInt32(SmoothY.Value);

								Vector2 vector = (from t in list
												  select t - new Vector2((float)(this.xSize / 2), (float)(this.ySize / 2)) into t
												  orderby t.Length()
												  select t).ElementAt(0) + new Vector2(1f, (float)this.offsetY);

								if (this.PixelSearch(new Rectangle((this.xSize - pixelx) / 2, (this.ySize - pixely) / 2, pixelx, pixely), pixel_Color, this.colorVariation).Length != 0)
								{

									int x = Convert.ToInt32(this.delayx);
									await Task.Delay(x);
									this.Move((int)(vector.X * (float)this.speed3), (int)(vector.Y * (float)this.speed3), pressDown);
									continue;
								}
								else
								{
									this.Move((int)(vector.X * (float)this.speed), (int)(vector.Y * (float)this.speed), pressDown);
									continue;
								}
							}
							catch (Exception ex)
							{
								Console.WriteLine("Main Ex." + ((ex != null) ? ex.ToString() : null));
								continue;
							}
							return;
						}

					}
				}

			}
		}

		public int GetColor(Form1.ColorType color)
		{
			if (color == Form1.ColorType.Red)
			{
				return 0x9A0000;
			}
			if (color != Form1.ColorType.Purple)
			{
				return 0xA224A2;
			}

			return 11480751;
		}

		private void UpdateDisplayInformation()
		{
			if (CustomScreen.Checked == true)
			{
				int x = int.Parse(ScreenX2.Text);
				int y = int.Parse(ScreenY2.Text);

				this.xSize = x;
				this.ySize = y;
			}
			else
			{
				this.zoom = this.GetScalingFactor();
				Screen screen = this.CurrentScreen();
				bool primary = screen.Primary;
				this.xSize = (int)((float)screen.Bounds.Width * (primary ? this.zoom : 1f));
				this.ySize = (int)((float)screen.Bounds.Height * (primary ? this.zoom : 1f));
			}
		}

		[DllImport("user32.dll")]
		private static extern void mouse_event(int dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

		public new void Move(int xDelta, int yDelta, bool pressDown = false)
		{
			if (pressDown)
			{
				if (DateTime.Now.Subtract(this.lastShot).TotalMilliseconds < (double)this.msShootTime)
				{
					pressDown = false;
				}
				else
				{
					this.lastShot = DateTime.Now;
				}
			}
			InjectedInputMouseInfo input =
		default(InjectedInputMouseInfo);
			input.DeltaX = xDelta;
			input.DeltaY = yDelta;
			input.MouseOptions = (InjectedInputMouseOptions)8196;
			if (pressDown)
			{
				input.MouseOptions = (InjectedInputMouseOptions)8194;
			}
			InputInjector.InjectMouseInput(input);
		}

		public Screen CurrentScreen()
		{
			return Screen.AllScreens[this.monitor];
		}

		public class DirectBitmap : IDisposable
		{
			public Bitmap Bitmap { get; private set; }
			public Int32[] Bits { get; private set; }
			public bool Disposed { get; private set; }
			public int Height { get; private set; }
			public int Width { get; private set; }

			protected GCHandle BitsHandle { get; private set; }

			public DirectBitmap(int width, int height)
			{
				Width = width;
				Height = height;
				Bits = new Int32[width * height];
				BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
				Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
			}

			public void SetPixel(int x, int y, Color colour)
			{
				int index = x + (y * Width);
				int col = colour.ToArgb();

				Bits[index] = col;
			}

			public Color GetPixel(int x, int y)
			{
				int index = x + (y * Width);
				int col = Bits[index];
				Color result = Color.FromArgb(col);

				return result;
			}

			public void Dispose()
			{
				if (Disposed) return;
				Disposed = true;
				Bitmap.Dispose();
				BitsHandle.Free();
			}
		}

		public unsafe Point[] PixelSearch(Rectangle rect, Color Pixel_Color, int Shade_Variation)
		{
			ArrayList arrayList = new ArrayList();
			using (var tile = new Bitmap(rect.Width, rect.Height, PixelFormat.Format16bppRgb555))
			{
				if (this.monitor >= Screen.AllScreens.Length)
				{
					this.monitor = 0;
					this.UpdateUI();
				}
				int left = Screen.AllScreens[this.monitor].Bounds.Left;
				int top = Screen.AllScreens[this.monitor].Bounds.Top;
				using (var g = Graphics.FromImage(tile))
				{
					g.CopyFromScreen(rect.X + left, rect.Y + top, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
				}
				BitmapData bitmapData = tile.LockBits(new Rectangle(0, 0, tile.Width, tile.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
				int[] array = new int[]
					{
					(int) Pixel_Color.B,
					(int) Pixel_Color.G,
					(int) Pixel_Color.R
				};

				for (int i = 0; i < bitmapData.Height; i++)
				{
					byte* ptr = (byte*)((void*)bitmapData.Scan0) + i * bitmapData.Stride;
					for (int j = 0; j < bitmapData.Width; j++)
					{
						if (((int)ptr[j * 3] >= array[0] - Shade_Variation & (int)ptr[j * 3] <= array[0] + Shade_Variation) && ((int)ptr[j * 3 + 1] >= array[1] - Shade_Variation & (int)ptr[j * 3 + 1] <= array[1] + Shade_Variation) && ((int)ptr[j * 3 + 2] >= array[2] - Shade_Variation & (int)ptr[j * 3 + 2] <= array[2] + Shade_Variation))
						{
							arrayList.Add(new Point(j + rect.X, i + rect.Y));
						}
					}
				}

				tile.UnlockBits(bitmapData); ;
				tile.Dispose();
				return (Point[])arrayList.ToArray(typeof(Point));

			}
		}

		private void Red_changed(object sender, EventArgs e)
		{
			this.color = Form1.ColorType.Red;
			this.SetKey("color", (int)this.color);
		}

		private void Purple_changed(object sender, EventArgs e)
		{
			this.color = Form1.ColorType.Purple;
			this.SetKey("color", (int)this.color);
		}

		private void Speed_changed(object sender, EventArgs e)
		{
			this.speed = this.Speed.Value;
			this.SetKey("speed", this.speed);
		}
		private void FovX_changed(object sender, EventArgs e)
		{
			this.fovX = (int)this.FovXNum.Value;
			this.SetKey("fovX", this.fovX);
		}

		private void FovY_changed(object sender, EventArgs e)
		{
			this.fovY = (int)this.FovYNum.Value;
			this.SetKey("fovY", this.fovY);
		}

		private void IsAimbot_changed(object sender, EventArgs e)
		{
			this.isAimbot = this.AimbotBtt.Checked;
			this.SetKey("isAimbot", this.isAimbot);
		}

		private void IsTriggerbot_changed(object sender, EventArgs e)
		{
			this.isTriggerbot = this.TriggerbotBtt.Checked;
			this.SetKey("isTriggerbot", this.isTriggerbot);
		}

		private void Main_load(object sender, EventArgs e)
		{
			this.mainThread = new Thread(delegate ()
			{
				this.xUpdate();
				this.xNoRecoil();
				this.xBhop();
				this.xPing();
				this.xTriggertest();
				this.xAimbot();
				this.xTriggerbot();
				this.xColorEsp();
				this.xPixelEsp();
			});
			this.mainThread.Start();
		}
		private void SetKey(string key, bool o)
		{
			Settings.Default[key] = o;
			Settings.Default.Save();
		}

		private void SetKey(string key, int o)
		{
			Settings.Default[key] = o;
			Settings.Default.Save();
		}

		private void SetKey(string key, decimal o)
		{
			Settings.Default[key] = o;
			Settings.Default.Save();
		}

		private T GetKey<T>(string key)
		{
			return (T)((object)Settings.Default[key]);
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.mainThread.Abort();
			Settings.Default.Save();
			base.OnHandleDestroyed(e);
		}



		private void Start_click(object sender, EventArgs e)
		{
			FormOverlay formoverlay = new FormOverlay();
			if (this.isRunning)
			{
				try
				{
					formoverlay.Close();
					FormOverlay obj = (FormOverlay)Application.OpenForms["FormOverlay"];
					obj.Close();
				}
				catch { }
			}
			this.isRunning = !this.isRunning;
			this.UpdateUI();
			if (CircleBtt.Checked == true)
			{
				try { formoverlay.Show(); }
				catch { }
			}
		}

		private void UpdateUI()
		{
			this.StartBtt.Text = (this.isRunning ? "Stop" : "Start");
			this.UpdateDisplayInformation();
			this.ChangeMonitorBtt.Text = string.Concat(new string[] {
				"Monitor [",
				this.monitor.ToString(),
				"] ",
				this.xSize.ToString(),
				"x",
				this.ySize.ToString()
			});
			this.AimkeyBtt.Text = Enum.GetName(typeof(Form1.AimKey), this.mainAimKey);
			this.TriggerKeyBtt.Text = Enum.GetName(typeof(Form1.Bhopkey), this.Bhopxkey);
		}

		private void MonitorChanged(object sender, EventArgs e)
		{
			this.monitor++;
			if (this.monitor >= Screen.AllScreens.Length)
			{
				this.monitor = 0;
			}
			this.SetKey("monitor", this.monitor);
			this.UpdateUI();
		}
		private void IsAimKeyChanged(object sender, EventArgs e)
		{
			this.isAimKey = this.AimKeyToggle.Checked;
			this.SetKey("isAimKey", this.isAimKey);
		}

		private void IsHold_changed(object sender, EventArgs e)
		{
			this.isHold = this.IsHoldToggle.Checked;
			this.SetKey("isHold", this.isHold);
		}

		private void AimKeyDrop(object sender, EventArgs e)
		{
			if (this.AimkeyBtt.PointToScreen(new Point(this.AimkeyBtt.Left, this.AimkeyBtt.Bottom)).Y + this.contextMenuStrip1.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
			{
				this.contextMenuStrip1.Show(this.AimkeyBtt, new Point(0, -this.contextMenuStrip1.Size.Height));
				return;
			}
			this.contextMenuStrip1.Show(this.AimkeyBtt, new Point(0, this.AimkeyBtt.Height));

		}

		private void TriggerKeyDrop(object sender, EventArgs e)
		{

			if (this.TriggerKeyBtt.PointToScreen(new Point(this.TriggerKeyBtt.Left, this.TriggerKeyBtt.Bottom)).Y + this.contextMenuStrip2.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
			{
				this.contextMenuStrip2.Show(this.TriggerKeyBtt, new Point(0, -this.contextMenuStrip2.Size.Height));
				return;
			}
			this.contextMenuStrip2.Show(this.TriggerKeyBtt, new Point(0, this.TriggerKeyBtt.Height));

		}

		private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e) { }

		private void ContextMenuStrip2_Opening(object sender, CancelEventArgs e) { }

		private void OffsetY_changed(object sender, EventArgs e)
		{
			this.offsetY = (int)this.offsetNum.Value;
			this.SetKey("offsetY", this.offsetY);
		}

		private void Label5_Click(object sender, EventArgs e) { }

		private void FireRate_changed(object sender, EventArgs e)
		{
			this.msShootTime = (int)this.FireRateNum.Value;
			this.SetKey("msShootTime", this.msShootTime);
		}

		public int xSize;

		public int ySize;

		public int msShootTime = 225;

		public DateTime lastShot = DateTime.Now;

		public int offsetY = 10;

		public bool isTriggerbot;

		public bool isPing;

		public bool isCall;

		public bool isAimbot;

		public bool isEsp;

		public bool isPixel;

		public bool isCircle;

		public bool isRecoil;

		public bool isBhop;

		public decimal PingX = 50;

		public decimal speed = 1m;

		public decimal speed3 = 1m;

		public decimal Bhop = 4;

		public decimal delayx = 100;

		public string circred = "1";

		public string circgreen;

		public string circblue;

		public int fovX = 100;

		public int FovCircleRed = 1;
		public int FovCircleGreen = 1;
		public int FovCircleBlue = 1;
		public int FovCircleWidth = 1;

		public bool closeme;

		public int fovY = 100;

		public bool isAimKey;

		public bool isHold = true;

		public int monitor;

		public int colorVariation = 25;

		private Form1.AimKey mainAimKey = Form1.AimKey.Alt;

		private Form1.Bhopkey Bhopxkey = Form1.Bhopkey.Alt;

		public Form1.ColorType color = Form1.ColorType.Purple;

		public float zoom = 1f;

		public const int MOUSEEVENTF_LEFTDOWN = 2;

		public const int MOUSEEVENTF_LEFTUP = 4;

		public const int MOUSEEVENTF_RIGHTDOWN = 8;

		public const int MOUSEEVENTF_RIGHTUP = 16;

		public Thread mainThread;

		public bool isRunning;
		public string myString;
		public string newString;
		public string callout;

		private enum AimKey
		{
			LeftMouse = 1,
			RightMouse,
			X1Mouse = 5,
			X2Button,
			Shift = 160,
			Ctrl = 162,
			Alt = 164,
			Capslock = 20,
			Numpad0 = 96,
			Numlock = 144
		}

		private enum Bhopkey
		{
			LeftMouse = 1,
			RightMouse,
			X1Mouse = 5,
			X2Button,
			Shift = 160,
			Ctrl = 162,
			Alt = 164,
			Capslock = 20,
			Numpad0 = 96,
			Numlock = 144
		}

		public enum DeviceCap
		{
			VERTRES = 10,
			DESKTOPVERTRES = 117
		}
		public enum ColorType
		{
			Red,
			Purple
		}

		private void Label7_Click(object sender, EventArgs e)
		{

		}

		private void ScreenX2_ValueChanged(object sender, EventArgs e)
		{

		}

		private void ScreenY2_ValueChanged(object sender, EventArgs e)
		{

		}

		private void Recoilcheckbox_CheckedChanged_1(object sender, EventArgs e)
		{
			this.isRecoil = this.RecoilBtt.Checked;
			this.SetKey("isRecoil", this.isRecoil);
		}

		private void CheckBox2_CheckedChanged(object sender, EventArgs e)
		{
			this.isBhop = this.Bhopbox.Checked;
			this.SetKey("isBhop", this.isBhop);
		}

		private void TextBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			this.Bhop = this.Bhopinput.Value;
			this.SetKey("Bhop", this.Bhop);
		}

		private void AimKeyToggle_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void CheckBox2_CheckedChanged_1(object sender, EventArgs e)
		{

		}

		private void CheckBox3_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void NumericUpDown1_ValueChanged_1(object sender, EventArgs e)
		{
			this.speed3 = this.Speed3.Value;
			this.SetKey("speed3", this.speed3);
		}

		private void NumericUpDown1_ValueChanged_2(object sender, EventArgs e)
		{
			this.delayx = this.Delayx.Value;
			this.SetKey("delayx", this.delayx);
		}

		private void PixelX_ValueChanged(object sender, EventArgs e)
		{

		}

		private void PixelY_ValueChanged(object sender, EventArgs e)
		{

		}

		private void Ping_CheckedChanged(object sender, EventArgs e)
		{
			if (Ping.Checked == true)
			{ MessageBox.Show("Change your Warning Ping to Keybind the Keybind you choosed in Communication Settings"); }

			this.isPing = this.Ping.Checked;
			this.SetKey("isPing", this.isPing);
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void checkBox2_CheckedChanged_2(object sender, EventArgs e)
		{

			if (EspBtt.Checked == true)
			{

				MessageBox.Show("This feature might slow down your Bot, especially if you use more than 2 Visuals at the same time. If you experience lags or stuttering Aimbot please disable this!");

			}

			this.isEsp = this.EspBtt.Checked;
			this.SetKey("isEsp", this.isEsp);
		}

		private void checkBox4_CheckedChanged(object sender, EventArgs e)
		{
			if (PixelBtt.Checked == true)
			{
				MessageBox.Show("This feature might slow down your Bot, especially if you use more than 2 Visuals at the same time. If you experience lags or stuttering Aimbot please disable this!");
			}

			this.isPixel = this.PixelBtt.Checked;
			this.SetKey("isPixel", this.isPixel);
		}

		private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
		{
			if (CircleBtt.Checked == true)
			{
				if (this.isRunning)
				{
					MessageBox.Show("Please Stop and Start the Cheat agine for the Fov Circle!");
				}
			}
			this.isCircle = this.CircleBtt.Checked;
			this.SetKey("isCircle", this.isCircle);

		}




		private void PingCode_ValueChanged(object sender, EventArgs e)
		{

		}

		private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
		{


		}

		private void CircleRed_ValueChanged(object sender, EventArgs e)
		{
			this.FovCircleRed = (int)this.CircleRed.Value;
			this.SetKey("FovCircleRed", this.FovCircleRed);
		}

		private void CircleWidth_ValueChanged(object sender, EventArgs e)
		{
			this.FovCircleWidth = (int)this.CircleWidth.Value;
			this.SetKey("FovCircleWidth", this.FovCircleWidth);
		}

		private void CircleGreen_ValueChanged(object sender, EventArgs e)
		{
			this.FovCircleGreen = (int)this.CircleGreen.Value;
			this.SetKey("FovCircleGreen", this.FovCircleGreen);
		}

		private void CircleBlue_ValueChanged(object sender, EventArgs e)
		{
			this.FovCircleBlue = (int)this.CircleBlue.Value;
			this.SetKey("FovCircleBlue", this.FovCircleBlue);
		}

		private void label9_Click(object sender, EventArgs e)
		{

		}

		private void label19_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}
	}
}