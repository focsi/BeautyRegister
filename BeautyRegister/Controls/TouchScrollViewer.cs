using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace BeautyRegister.Controls
{
    /// <summary>
    /// Osztály, ami megakadályozza, hogy egyből el kezdjen scrollozni a listview, ha kézzel húzom el.
    /// Ezáltal lehetősgé van megynomni a kontrollokat akkor is, ha kicsit megcsúszik a keze
    /// </summary>
    class TouchScrollViewer : ScrollViewer
    {
        protected override void OnManipulationDelta( System.Windows.Input.ManipulationDeltaEventArgs e )
        {
            if ( ( IsDeltaSmall( e ) ) )
            {
                DontScroll( e );
            }   
            else
            {
                base.OnManipulationDelta( e );
            }
        }

        private static bool IsDeltaSmall( System.Windows.Input.ManipulationDeltaEventArgs e )
        {
            const double pixels = 20;
            return e.CumulativeManipulation.Translation.Length < pixels
                                && e.CumulativeManipulation.Scale.X == 1;
        }

        private static void DontScroll( System.Windows.Input.ManipulationDeltaEventArgs e )
        {
            e.Handled = true;
        }
    }
}
