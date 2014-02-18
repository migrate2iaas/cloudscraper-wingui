using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CloudScraper
{
    public class ToolTipContainer
    {
        #region Data members

        private readonly List<ToolTip2Ctrl> list_;
        private readonly ToolTip toolTip_;

        #endregion Data members
         
        #region Constructors

        public ToolTipContainer(ToolTip toolTip)
        {
            toolTip_ = toolTip;
            list_ = new List<ToolTip2Ctrl>();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Sets a tool tip for the control. 
        /// 1. When the control Text property is not set, the default tool tip text is shown. 
        /// 2. When the control Text property is set and the alternative text is not null, it 
        /// is shown as a tool tip (empty string is a valid value). If the alternative text
        /// is null, the control Text property is shown in the tool tip.
        /// </summary>
        /// <param name="ctrl">The control to set tool tip for.</param>
        /// <param name="defaultText">A text to show in the tool tip when
        /// the control Text property is empty.</param>
        /// <param name="alternativeText">A text to show in the tool tip when
        /// the control Text property is non-empty. Null means show the control
        /// Text property in the tool tip.</param>
        public void Create(Control ctrl, string defaultTex, string alternativeText)
        {
            ToolTip2Ctrl item = new ToolTip2Ctrl(toolTip_, ctrl, defaultTex, alternativeText);
            MyRemove(item);
            list_.Add(item);
        }

        public void Remove(Control ctrl)
        {
            ToolTip2Ctrl dummy = new ToolTip2Ctrl(toolTip_, ctrl, null, null);
            MyRemove(dummy);
        }

        #endregion Public methods

        #region Private methods

        private void MyRemove(ToolTip2Ctrl item)
        {
            int removeAt = list_.FindIndex(delegate(ToolTip2Ctrl findMe)
            {
                return findMe.Equals(item);
            });

            if (-1 != removeAt)
            {
                list_[removeAt].Dispose();
                list_.RemoveAt(removeAt);
            }
        }

        #endregion Private methods

        #region Tool tip to control mapping

        private class ToolTip2Ctrl : IDisposable
        {
            #region Data members

            private ToolTip toolTip_;
            private Control ctrl_;

            private readonly string defaultText_;
            private readonly string alternativeText_;

            #endregion Data members

            /// <summary>
            /// Sets a tool tip for the control. 
            /// 1. When the control Text property is not set, the default tool tip text is shown. 
            /// 2. When the control Text property is set and the alternative text is not null, it 
            /// is shown as a tool tip (empty string is a valid value). If the alternative text
            /// is null, the control Text property is shown in the tool tip.
            /// </summary>
            /// <param name="toolTip">The tool tip component.</param>
            /// <param name="ctrl">The control to set tool tip for.</param>
            /// <param name="defaultText">A text to show in the tool tip when
            /// the control Text property is empty.</param>
            /// <param name="alternativeText">A text to show in the tool tip when
            /// the control Text property is non-empty. Null means show the control
            /// Text property in the tool tip.</param>
            public ToolTip2Ctrl(ToolTip toolTip, Control ctrl, string defaultText, string alternativeText)
            {
                toolTip_ = toolTip;
                ctrl_ = ctrl;
                defaultText_ = defaultText;
                alternativeText_ = alternativeText;

                ctrl_.MouseEnter += new EventHandler(OnMouseEnter);
                ctrl_.MouseHover += new EventHandler(OnMouseHover);
            }

            #region IDisposable members

            public void Dispose()
            {
                ctrl_.MouseEnter -= new EventHandler(OnMouseEnter);
                ctrl_.MouseHover -= new EventHandler(OnMouseHover);
            }

            #endregion IDisposable members

            #region Base overrides

            public override bool Equals(object obj)
            {
                return (obj is ToolTip2Ctrl) ? ctrl_ == ((ToolTip2Ctrl)obj).ctrl_ : base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return null != ctrl_ ? ctrl_.GetHashCode() : base.GetHashCode();
            }

            #endregion Base overrides

            #region Private methods

            private void MySetToolTip()
            {
                string text = !string.IsNullOrEmpty(ctrl_.Text) ? alternativeText_ : defaultText_;
                if (null == text)
                {
                    text = string.Empty;
                }
                toolTip_.SetToolTip(ctrl_, text);
            }

            private void OnMouseEnter(object sender, EventArgs e)
            {
                MySetToolTip();
            }

            private void OnMouseHover(object sender, EventArgs e)
            {
                MySetToolTip();
            }

            #endregion Private methods
        }

        #endregion Tool tip to control mapping
    }
}
