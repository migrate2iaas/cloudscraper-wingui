using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using NLog;

namespace CloudScraper
{
    public class ControlDebugPrintoutContainer
    {
        #region Data members

        private readonly Logger logger_;
        private readonly List<ControlToDebugPrintout> list_ = new List<ControlToDebugPrintout>();

        #endregion Data members

        public ControlDebugPrintoutContainer(Logger logger)
        {
            logger_ = logger;
        }

        #region Public members

        public void Create(Control ctrl, string tag)
        {
            ControlToDebugPrintout ctrl2debugPrintout = new ControlToDebugPrintout(logger_, ctrl, tag);
            MyRemove(ctrl2debugPrintout);
            list_.Add(ctrl2debugPrintout);
        }

        public void Remove(Control ctrl)
        {
            ControlToDebugPrintout dummy = new ControlToDebugPrintout(logger_, ctrl, null);
            MyRemove(dummy);
        }

        #endregion Public members

        #region Private methods

        private void MyRemove(ControlToDebugPrintout item)
        {
            int removeAt = list_.FindIndex(delegate(ControlToDebugPrintout findMe)
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

        #region Control to Debug printout mapping

        private class ControlToDebugPrintout : IDisposable
        {
            private readonly Logger logger_;
            private readonly Control ctrl_;
            private readonly string tag_;

            public ControlToDebugPrintout(Logger logger, Control ctrl, string tag)
            {
                logger_ = logger;
                ctrl_ = ctrl;
                tag_ = tag;

                ctrl_.Leave += new EventHandler(OnLeave);
            }

            #region IDisposable members

            public void Dispose()
            {
                ctrl_.Leave -= OnLeave;
            }

            #endregion IDisposable members

            public bool ContainsObject(Control ctrl)
            {
                return ctrl_ == ctrl;
            }

            public override bool Equals(object obj)
            {
                return (null != obj && obj is ControlToDebugPrintout) ? ((ControlToDebugPrintout)obj).ctrl_ == ctrl_ : base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return (null != ctrl_) ? ctrl_.GetHashCode() : base.GetHashCode();
            }

            private void OnLeave(object sender, EventArgs e)
            {
                if (logger_.IsDebugEnabled)
                {
                    logger_.Debug(string.Format("{0}{1}", tag_, ctrl_.Text));
                }
            }
        }

        #endregion Control to Debug printout mapping
    }
}
