using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UtilityLibrary.Forms
{
    [DefaultEvent("Click")]
    public partial class RoundedButton : UserControl
    {
        // http://hkpr.info/dotnet/RoundButton/

        
        #region region - public property: design

        [Category("Appearance")]
        [Browsable(true)]
        [Description("角の丸さ長さの比率を指定します。")]
        public float CornerRatio
        {
            get => this._cornerRatio;
            set
            {
                if (value > 1.0) value = 1.0f;
                if (value < 0) value = 0.0f;

                this._cornerRatio = value;
            }
        }
        private float _cornerRatio = 0.2f;



        [Category("Appearance")]
        [Browsable(true)]
        [Description("境界線の太さの比率を指定します。")]
        public float BorderRatio
        {
            get => this._borderRatio;
            set
            {
                if (value > _cornerRatio) value = _cornerRatio;
                if (value < 0) value = 0.0f;

                this._borderRatio = value;
            }
        }
        private float _borderRatio = 0.15f;


        [Description("ボタン押下時などの色の暗さの度合いを指定します。")]
        public float BackColorContrastRatio
        {
            get => this._borderRatio;
            set
            {
                if (value > 0.3f) value = 0.3f;
                if (value < 0.1f) value = 0.1f;

                this._backColorContrastRatio = value;
            }
        }
        private float _backColorContrastRatio = 0.20f;


        public float SurfaceTransparentRate
        {
            get => this._surfaceAlpha / 255.0f;
            set
            {
                if (value > 0.9f) value = 0.9f;
                if (value < 0.5f) value = 0.5f;

                this._surfaceAlpha = (int)(255 * (1.0f - value));
            }
        }
        private int _surfaceAlpha = 51;


        public int Alpha
        {
            get => this._alpha;

            set
            {
                if (value > 255) value = 255;
                if (value < 0) value = 0;

                this._alpha = value;
            }
        }
        private int _alpha = 230;


        public virtual bool Blink
        {
            get => this.blinkTimer.Enabled;
            set
            {
                this._currentBlinkIndex = 0;
                this.blinkTimer.Enabled = value;
            }
        }

        [Browsable(false)]
        public virtual bool AttentionMode
        {
            get => this._attentionMode;
            set
            {
                this._attentionMode = value;
            }
        }
        public bool _attentionMode = true;


        [Description("コントロールに関連付けられたテキストです。")]
        public override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }





        #endregion


        #region region - public properties : colour

        // ForeColor => 文字色
        // BackColor => Content 内の背景色


        [Category("Appearance")]
        [Browsable(true)]
        [Description("コンテナ部の背景色を指定します。")]
        public Color ContainerColor { get; set; } = Color.SteelBlue;



        [Category("Appearance")]
        [Browsable(true)]
        [Description("フォーカス時の境界線の色を指定します。")]
        public Color FocusBorderColor
        {
            get => this._focusBorderColor;
            set
            {
                this._focusBorderColor = value;
            }
        }
        private Color _focusBorderColor = Color.FromArgb(48, 48, 48);



        [Category("Appearance")]
        [Browsable(true)]
        [Description("境界線の色を指定します。")]
        public Color BorderColor {

            get => this._borderColor;
            set
            {
                this._borderColor = value;
            }
        }
        private Color _borderColor = Color.DimGray;



        [Category("Appearance")]
        [Browsable(true)]
        [Description("フォーカス時の表面の色を指定します。")]
        public Color FocusSurfaceColor
        {

            get => this._focusSurfaceColor;
            set
            {
                this._focusSurfaceColor = value;
            }
        }
        private Color _focusSurfaceColor = System.Drawing.SystemColors.Highlight;



        [Category("Appearance")]
        [Browsable(true)]
        [Description("フォーカス時の表面の色を指定します。")]
        public Color HighlightColor
        {

            get => this._highlightColor;
            set
            {
                this._highlightColor = value;
            }
        }
        private Color _highlightColor = System.Drawing.SystemColors.ButtonHighlight;


        public Color BlinkColor = Color.White;


        public Color AttentionColor
        {
            get => this._attentionColor;
            set
            {
                this._attentionColor = value;
            }
        }
        private Color _attentionColor = Color.Red;



        #endregion


        #region region - public properties : main function


        [Description("点滅時の点滅周期を設定します。（単位 mSec）")]
        public int BlinkInterval
        {
            set => this.blinkTimer.Interval = value;
            get => this.blinkTimer.Interval;
        }


        [Description("グループ変更の ContextMenuStrip を使用の可否を設定します。")]
        public bool UseContextMenuStrip { get; set; } = true;



        [System.ComponentModel.Browsable(false)]  // デザイナには表示しない
        public int MachineID { get; set; }

        
        
        #endregion
        

        #region region - private, protected member : design

        /// <summary>
        /// 角の丸の長さ（半径）です。
        /// </summary>
        protected virtual int CurvedCornerLength
        {
            get
            {
                // 幅と高さで短い方を取得
                int clientLength = Math.Min(this.ClientSize.Width, this.ClientSize.Height);

                // 長さの半分を取得する（切り上げ）　半分にしておかないと、0.5 (=50%) を超えた値に対応できない。
                int halfLength = (int)Math.Round(clientLength / 2.0);

                return (int)(halfLength * this._cornerRatio);
            }
        }


        protected virtual int BorderThickness
        {
            get
            {
                // 幅と高さで短い方を取得
                int clientLength = Math.Min(this.ClientSize.Width, this.ClientSize.Height);

                // 長さの半分を取得する（切り上げ）　半分にしておかないと、0.5 (=50%) を超えた値に対応できない。
                int halfLength = (int)Math.Round(clientLength / 2.0);

                return (int)(halfLength * this._borderRatio);
            }
        }


        protected virtual float[] BlinkingRatioArray
        {
            get => new float[] { 1.0f, 0.4f };
        }

        private int _currentBlinkIndex = 0;

        private float _currentBlinkRatio { get => this.BlinkingRatioArray[this._currentBlinkIndex]; }

        #endregion


        #region region - private member : main function

        private bool _isMouseDowning = false;

        private bool _isMouseHovering = false;

        private bool _isSizeChangingByProgram = false;

        private Keys _holdingKey = Keys.None;

        private Timer blinkTimer;

        #endregion



        [System.ComponentModel.Browsable(false)]  // デザイナには表示しない
        public Rectangle PaddingRectangle { get; set; }

        [System.ComponentModel.Browsable(false)]  // デザイナには表示しない
        public Rectangle ContentRectangle { get; set; }





        public RoundedButton()
        {
            InitializeComponent();


            this.blinkTimer = new System.Windows.Forms.Timer();
            this.blinkTimer.Tick += BlinkTimer_Tick;
            this.blinkTimer.Interval = 500;
            this.blinkTimer.Enabled = false;
        }

        private void BrowsingMapWeavingMachinPannel_LostFocus(object sender, EventArgs e)
        {
            this.Refresh();
        }


        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            if (this._currentBlinkIndex >= (this.BlinkingRatioArray.Length - 1))
            {
                _currentBlinkIndex = 0;
            }
            else
            {
                _currentBlinkIndex += 1;
            }

            this.Refresh();
        }


        /*
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;   //  WS_EX_TRANSPARENT
                return cp;
            }
        }*/



        #region region - inherit methods: 

        protected override void OnPaint(PaintEventArgs e)
        {

            #region region - 3つの rectangle の設定


            var thisSize = this.Size;
            var clientSize = this.ClientSize;

            // container の範囲を示す rectangle
            // ※ ClientSize = 視覚テーマに依存しない領域
            var innerRect = new Rectangle(
                this.ClientRectangle.X + base.Margin.Left,
                this.ClientRectangle.Y + base.Margin.Top,
                this.ClientRectangle.Width - base.Margin.Horizontal,
                this.ClientRectangle.Height - base.Margin.Vertical
                );


            this.PaddingRectangle = new Rectangle(
                    innerRect.X + this.BorderThickness,
                    innerRect.Y + this.BorderThickness,
                    innerRect.Width - this.BorderThickness * 2,
                    innerRect.Height - this.BorderThickness * 2
                    );


            this.ContentRectangle = new Rectangle(
                 this.PaddingRectangle.X + this.Padding.Left,
                 this.PaddingRectangle.Y + this.Padding.Top,
                 this.PaddingRectangle.Width - this.Padding.Horizontal,
                 this.PaddingRectangle.Height - this.Padding.Vertical
                 );

            #endregion


            using (var path_region = new System.Drawing.Drawing2D.GraphicsPath())
            {

                #region region -  Region の path

                // 半径
                int radius = this.CurvedCornerLength;

                // 直径
                int diameter = radius * 2;

                if (radius > 0)
                {
                    // ※ AddArc, Add Line などは追加する順序も影響する

                    // 左上
                    path_region.AddArc
                        (innerRect.X,
                        innerRect.Y,
                        diameter, diameter,
                        180, 90);

                    // 右上
                    path_region.AddArc
                        (innerRect.X + innerRect.Width - diameter,
                        innerRect.Y,
                        diameter, diameter,
                        270, 90);

                    // 右下
                    path_region.AddArc
                        (innerRect.X + innerRect.Width - diameter,
                        innerRect.Y + innerRect.Height - diameter,
                        diameter, diameter,
                        0, 90);

                    // 左下
                    path_region.AddArc
                        (innerRect.X,
                        innerRect.Y + innerRect.Height - diameter,
                        diameter, diameter,
                        90, 90);

                    // 左上へ行って戻る：線というより点
                    path_region.AddLine(
                        innerRect.X,
                        innerRect.Y + radius,
                        innerRect.X,
                        innerRect.Y + radius);

                    path_region.CloseFigure();
                }
                else
                {
                    path_region.AddRectangle(innerRect);
                }
                #endregion


                // このコントロールの領域を設定
                this.Region = new Region(path_region);


                using (var path_content = new System.Drawing.Drawing2D.GraphicsPath())
                {

                    #region region - content の path

                    // 半径
                    int radius_inner = this.CurvedCornerLength - this.BorderThickness;

                    // 直径
                    int diameter_inner = radius_inner * 2;


                    if (radius_inner > 0)
                    {
                        // 左上
                        path_content.AddArc
                            (this.PaddingRectangle.X,
                            this.PaddingRectangle.Y,
                            diameter_inner, diameter_inner,
                            180, 90);

                        // 右上
                        path_content.AddArc
                            (this.PaddingRectangle.X + this.PaddingRectangle.Width - diameter_inner,
                            this.PaddingRectangle.Y,
                            diameter_inner, diameter_inner,
                            270, 90);

                        // 右下
                        path_content.AddArc
                            (this.PaddingRectangle.X + this.PaddingRectangle.Width - diameter_inner,
                            this.PaddingRectangle.Y + this.PaddingRectangle.Height - diameter_inner,
                            diameter_inner, diameter_inner,
                            0, 90);

                        // 左下
                        path_content.AddArc
                            (this.PaddingRectangle.X,
                            this.PaddingRectangle.Y + this.PaddingRectangle.Height - diameter_inner,
                            diameter_inner, diameter_inner,
                            90, 90);


                        // 左上へ行って戻る：線というより点
                        path_content.AddLine(
                            this.PaddingRectangle.X,
                            this.PaddingRectangle.Y + radius_inner,
                            this.PaddingRectangle.X,
                            this.PaddingRectangle.Y + radius_inner);

                        // path を閉じて図形を完成させる
                        path_content.CloseFigure();

                    }
                    else
                    {
                        path_content.AddRectangle(new Rectangle(
                            this.PaddingRectangle.X,
                            this.PaddingRectangle.Y,
                            this.PaddingRectangle.Width,
                            this.PaddingRectangle.Height));
                    }
                    #endregion


                    // 今回使用する背景色
                    int r = (int)(this.ContainerColor.R * _currentBlinkRatio + BlinkColor.R * (1.0 - _currentBlinkRatio));
                    int g = (int)(this.ContainerColor.G * _currentBlinkRatio + BlinkColor.G * (1.0 - _currentBlinkRatio));
                    int b = (int)(this.ContainerColor.B * _currentBlinkRatio + BlinkColor.B * (1.0 - _currentBlinkRatio));

                    // 値修正
                    r = r > 255 ? 255 : r;
                    g = g > 255 ? 255 : g;
                    b = b > 255 ? 255 : b;

                    // 今回使用する背景色
                    Color color_container = Color.FromArgb(this._alpha, r, g, b);


                    // BackColor を若干濃くした色
                    var color_dark = Color.FromArgb(
                            this._alpha,
                            (int)(color_container.R * (1.0f - this._backColorContrastRatio)),
                            (int)(color_container.G * (1.0f - this._backColorContrastRatio)),
                            (int)(color_container.B * (1.0f - this._backColorContrastRatio)));


                    // 背景色の選択
                    if (this._isMouseDowning)
                        color_container = color_dark;

                    // Content 部分の背景色を塗りつぶす
                    using (var brush = new SolidBrush(color_container))
                    {
                        e.Graphics.FillPath(brush, path_content);
                    }


                    #region region - Border 描写

                    using (var path_border = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        path_border.AddPath(path_region, true);

                        path_border.AddPath(path_content, true);


                        // 今回使用する Border の色 
                        //var color_border = this.BorderColor;
                        // ↓ 18.01.17 変更
                        var color_border = base.Focused?
                            this._focusBorderColor : this._borderColor;

                        if (this._attentionMode)
                            color_border = this._attentionColor;



                        // Border の描写
                        using (var brush = new SolidBrush(color_border))
                        {
                            e.Graphics.FillPath(brush, path_border);
                        }

                        // hovering の薄膜の描写
                        if (this._isMouseHovering)
                        {
                            var surfaceColor = Color.FromArgb(this._surfaceAlpha, this.FocusSurfaceColor);

                            using (var brush = new SolidBrush(surfaceColor))
                            {
                                e.Graphics.FillPath(brush, path_border);
                            }
                        }
                    }

                    #endregion


                    #region region - 非選択状態の描写

                    if (!base.Focused)
                    {
                        using (var hatchBrush = new System.Drawing.Drawing2D.HatchBrush
                                (System.Drawing.Drawing2D.HatchStyle.Percent30, color_container, color_dark))
                        {
                            // 塗りつぶし
                            e.Graphics.FillPath(hatchBrush, path_content);
                        }
                    }

                    #endregion


                    #region region  - ハイライトの描写

                    if (this.Enabled)
                    {
                        using (var path_highlight = new System.Drawing.Drawing2D.GraphicsPath())
                        {
                            #region region - highlight


                            // int harfHeight = (int)(this.Height / 2);
                            // ↓ 17.12.27 修正
                            int harfHeight = this.PaddingRectangle.Height / 2;

                            if (radius_inner > 0)
                            {
                                // 左上
                                path_highlight.AddArc(
                                this.PaddingRectangle.X,
                                this.PaddingRectangle.Y,
                                diameter_inner,
                                diameter_inner,
                                180, 90);

                                // 右上へ
                                path_highlight.AddArc(
                                    this.PaddingRectangle.X + this.PaddingRectangle.Width - diameter_inner,
                                    this.PaddingRectangle.Y,
                                    diameter_inner,
                                    diameter_inner,
                                    270, 90);


                                // 右下へ
                                path_highlight.AddLine(
                                     this.PaddingRectangle.Width + this.PaddingRectangle.X,
                                    this.PaddingRectangle.Y + radius_inner,
                                    this.PaddingRectangle.Width + this.PaddingRectangle.X,
                                    harfHeight);

                                // 左下へ
                                path_highlight.AddLine(
                                    this.PaddingRectangle.X,
                                    harfHeight,
                                    this.PaddingRectangle.X,
                                    harfHeight);

                                // path を閉じて図形を完成させる
                                path_highlight.CloseFigure();


                                #endregion

                            }
                            else
                            {
                                path_highlight.AddRectangle(new Rectangle(
                                    this.PaddingRectangle.X,
                                    this.PaddingRectangle.Y,
                                    this.PaddingRectangle.Width,
                                    harfHeight));

                                // path を閉じて図形を完成させる
                               // path_highlight.CloseFigure();

                            }

                            /*
                            // ハイライト用のブラシ
                            var brush_highlight = new System.Drawing.Drawing2D.LinearGradientBrush(
                                // new Point(0, 0),
                                // new Point(0, harfHeight),
                                new Point(this.PaddingRectangle.X, this.PaddingRectangle.Y),
                                new Point(this.PaddingRectangle.X, this.PaddingRectangle.Y + harfHeight),
                                Color.FromArgb(255, this._highlightColor),
                                Color.FromArgb(0, this.ContainerColor)
                                );
                            */

                            // ハイライト用のブラシ
                            // グラデーションの方向：          第１引数から第２引数の方向へ
                            // グラデーションの色の変わり方：  第３引数から第４引数へ
                            // このパターンの繰り返しで指定範囲を描写する
                            var brush_highlight = new System.Drawing.Drawing2D.LinearGradientBrush(
                                new Point(this.PaddingRectangle.X, this.PaddingRectangle.Y),
                                new Point(this.PaddingRectangle.X, this.PaddingRectangle.Y + harfHeight),
                                Color.FromArgb(255, this._highlightColor),
                                Color.FromArgb(0, this.ContainerColor)
                                );



                            /*
                            // ↓ サイズ変更したらハイライトの描写がおかしくなったので修正
                            var highlightRectangle = new Rectangle(
                                this.PaddingRectangle.X,
                                this.PaddingRectangle.Y,
                                this.PaddingRectangle.Width,
                                harfHeight);

                            var brush_highlight = new System.Drawing.Drawing2D.LinearGradientBrush(
                                highlightRectangle,
                                Color.FromArgb(255, this._highlightColor),
                                Color.FromArgb(0, this.ContainerColor),
                                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                                */
                            e.Graphics.FillPath(brush_highlight, path_highlight);
                        }
                    }
                    #endregion


                    #region region - 文字の描写: 未確認 17.12.08

                    /*
                    // 文字列が描画領域に収まるように調整
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sbm = new StringBuilder();

                    foreach (char c in base.Text)
                    {
                        sbm.Append(c);

                        Size size = TextRenderer.MeasureText(sbm.ToString(), this.Font);


                        if (size.Width > this.ContentRectangle.Width - this.Font.Size)
                        {
                            sbm.Remove(sbm.Length - 1, 1);

                            sbm.Append(c);

                            sbm.AppendLine("");

                            sb.Append(sbm.ToString());

                            sbm = new StringBuilder();
                        }
                    }


                    sb.Append(sbm.ToString());

                    TextRenderer.DrawText(
                        e.Graphics,
                        sb.ToString(),
                        this.Font,
                        this.ContentRectangle,
                        this.ForeColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                        
                     */
                    #endregion


                    // 未使用時(Enabled = false) 描写
                    if (!this.Enabled)
                    {
                        Color color_unable = System.Drawing.SystemColors.Control;

                        using (var hatchBrush = new System.Drawing.Drawing2D.HatchBrush
                                (System.Drawing.Drawing2D.HatchStyle.Percent50, color_unable, color_container))
                        {
                            // 塗りつぶし
                            e.Graphics.FillPath(hatchBrush, path_content);
                        }
                    }
                }
            }
            //base.OnPaint(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            this.Refresh();
            //base.OnEnabledChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (this._isSizeChangingByProgram) return;
            
            if (this.Size.Width < 10 || this.Size.Height < 10)
            {
                int width = this.Size.Width >= 10 ? this.Size.Width : 10;
                int height = this.Size.Height >= 10 ? this.Size.Height : 10;

                this._isSizeChangingByProgram = true;

                this.Size = new Size(width, height);

                this._isSizeChangingByProgram = false;
            }
            //base.OnSizeChanged(e);
        }


        #endregion



        #region region - inherit methods: mouse, keyboard

        protected override void OnMouseEnter(EventArgs e)
        {
            this._isMouseHovering = true;
            this.Refresh();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this._isMouseHovering = false;
            this.Refresh();
            base.OnMouseLeave(e);
        }
        protected override void OnMouseHover(EventArgs e)
        {
            // マウスが一定時間以上動かなかった時のみ発生。但しMosuerEnter から MouserLeave までの間で一回だけ
            base.OnMouseHover(e);
        }
        /*
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // MosuerEnter から MouserLeave までの間で何度か発生する
            base.OnMouseMove(e);
        }*/


        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (this._isMouseDowning)
            {
                this._isMouseDowning = false;
                this.Refresh();
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!this._isMouseDowning)
            {
                this._isMouseDowning = true;
                this.Refresh();
            }
            base.OnMouseDown(e);
        }

        // フォーカスを失った時　※OnLostFocus よりこちらのほうがいいらしい
        protected override void OnLeave(EventArgs e)
        {
            this.Refresh();
        }
    
        // フォーカスを得た時　※OnGotFocus よりこちらのほうがいいらしい
        protected override void OnEnter(EventArgs e)
        {
            this.Refresh();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            this._holdingKey = e.KeyCode;

            // .NET の Button と動きを合わせる Space を押した時だけボタン押下の描写をする
            if (!this._isMouseDowning && (e.KeyCode == Keys.Space /* || e.KeyCode == Keys.Enter*/))
            {
                this._isMouseDowning = true;
                this.Refresh();
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            this._holdingKey = Keys.None;

            if (this._isMouseDowning && (_holdingKey == e.KeyCode))
            {
                this._isMouseDowning = false;
                this.Refresh();
            }
            base.OnKeyUp(e);
        }


        #endregion


    }
}

// 半透明について 17.12.08
// Form には　Opacity　というプロパティがあって簡単にできるが、
// コントロールにはない。基本的には下の画像を取得してきて、最初に描写して上に他の色を重ねていく
// ただし、キャプチャに１秒ほどの時間がかかるので使い物にならない
/*
       var parent = this.Parent;

                   //コントロールの外観を描画するBitmapの作成
                   using (var bmp = new Bitmap(parent.Width, parent.Height))
                   {
                   // めちゃくちゃ重たい

                       //キャプチャする
                       parent.DrawToBitmap(bmp, new Rectangle(0, 0, parent.Width, parent.Height));

                       bmp.Save(@"C:\data\1.png");
                   }


                   */
