using System;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Primitives;


using System.Windows;


namespace BeautyRegister.Controls
{
    using KodLista = System.Collections.Generic.List<OptionListItem>;

    /// <summary>
    /// Interaction logic for OptionList.xaml
    /// </summary>
    public partial class OptionList : UserControl
    {
        private const int GombMaxMeret = 70;
                
        public static event EventHandler<OLSelectionChangedEventArgs> EH;

        #region Enums
        /// <summary>
        /// A megvalósítás leheséges módjai.
        /// </summary>
        public enum DisplayModeEnum
        {
            /// <summary>
            /// RadioButton-szerűen működő gombokkal (ha a lehetséges értékek száma ezt lehetővé teszi)
            /// </summary>
            RadioButtons,

            /// <summary>
            /// Egy darab ComboBox-szal (a lehetséges értékek számától függetleül)
            /// </summary>
            ComboBox
        }
        
        #endregion

        #region Members
        /// <summary>
        /// A felhasználó álatl kívánt megjelenítési mód.
        /// <para>Nem biztos, hogy lehetséges ezt használni.</para>
        /// <para>A tényleges megjelenítési módot az m_CurrentDisplayMode tartalmazza.</para>
        /// </summary>
        private DisplayModeEnum m_UserDisplayMode=DisplayModeEnum.RadioButtons;


        /// <summary>
        /// A tényleges megjelenítési mód.
        /// <para>Az m_UserDisplayMode és a megjelenítendő lehetőségek számától függően kerül meghatározásra.</para>
        /// </summary>
        private DisplayModeEnum m_CurrentDisplayMode=DisplayModeEnum.RadioButtons;

        /// <summary>
        /// Combo box objektum. Azért ez az elnevezés, hogy a Halasz_Mobil forrásával legalább ez egyezzen
        /// </summary>
        private ComboBox cmb_List;

        #endregion
        
        #region Konstruktor
        public OptionList()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        private Byte m_MaxNumberOfButtons = 5;

        private int m_ButtonHorizontalGap=15;

        /// <summary>
        /// A kiválasztott OptionListItem objektum.
        /// </summary>
        private OptionListItem m_SelectedItem=null;

        
        [Category( "Layout" ), Description( "A megjelenítési mód." )]
        public DisplayModeEnum DisplayMode
        {
            get { return m_UserDisplayMode; }
            set
            {
                m_UserDisplayMode = value;
                Set_CurrentDisplayMode();
            }
        }


        /// <summary>
        /// A tényleges megjelenítési mód.
        /// <para>Az DisplayMode és a megjelenítendő lehetőségek számától függően kerül meghatározásra.</para>
        /// </summary>
        internal DisplayModeEnum CurrentDisplayMode
        {
            get { return m_CurrentDisplayMode; }
            private set
            {
                m_CurrentDisplayMode = value;
                switch ( m_CurrentDisplayMode )
                {
                    case DisplayModeEnum.RadioButtons:
                        CreateControl_Buttons();
                        break;
                    case DisplayModeEnum.ComboBox:
                        CreateControl_Combo();
                        break;
                    default:
                        CreateControl_Combo();
                        break;
                }
            }
        }



        [Category( "Layout" ), Description( "Legfeljebb ennyi gombot jelenítünk meg.\nHa ennél több lehetséges érték van, akkor ComboBox fogja őket megjeleníteni." )]
        public Byte MaxNumberOfButtons
        {
            get { return m_MaxNumberOfButtons; }
            set { m_MaxNumberOfButtons = value; }
        }

    
        /// <summary>
        /// A kiválasztott OptionListItem objektum
        /// </summary>
        [Category( "Data" ), Description( "A kiválasztott bejegyzés (mint OptionListItem objektum)." )]
        public OptionListItem SelectedItem
        {
            get
            {
                return m_SelectedItem;
            }
            set
            {
                if ( m_SelectedItem == value )
                    return;

                m_SelectedItem = value;

                // valid?
                if ( m_SelectedItem != null )
                    SelectedValue = m_SelectedItem.Value;
                else
                    SelectedValue = null;

                // 
                OnSelectionChangedEvent();
            }
            
        }


        #endregion
        
        #region Metódusok

        /// <summary>
        /// A felhasználói igény és a lehetőségek figyelembe vételével meghatározza, 
        /// hogy milyen megjelenítési módot alkalmazzunk.
        /// </summary>
        private void Set_CurrentDisplayMode()
        {
            //Ha Combot-akar, vagy meg nincsenek megjelenitendo elemek, akkor Combo
            if (   m_UserDisplayMode == DisplayModeEnum.ComboBox
                || Items == null
                || Items.Count == 0 )
            {
                CurrentDisplayMode = DisplayModeEnum.ComboBox;
                return;
            }

            //Ha Button-t akar, akkor a megjelenitendo elemek szamatol fugg a dontes.
            if (Items.Count > m_MaxNumberOfButtons)
                CurrentDisplayMode = DisplayModeEnum.ComboBox;
            else
                CurrentDisplayMode = DisplayModeEnum.RadioButtons;

        }


        /// <summary>
        /// Elenőrzi, hogy a beállítandó érték szerepel-e a lehetséges értékek listájában.
        /// <para>Ha nem szerepel, akkor ArgumentOutOfRangeException-t dob.</para>
        /// </summary>
        /// <param name="value"></param>
        private void CheckIfNewValueIsInList( double? value )
        {
            if ( Items == null )
            {
                //TODO App.Logger.Error( "CheckIfNewValueIsInList: A lista nincs beállítva." );
            }
            else
            {
                if (value == null) return;
                var Item = Items.FirstOrDefault( I => I.Value == value );
                if ( Item == null )
                {
                    //TODO App.Logger.Error( String.Format( "A lista nem tartalmaz '{0}' értéket, így a megadott érték nem állítható be.", value ) );
                }
            }
        }

        #endregion

        #region SelectionChange
        /// <summary>
        /// A kijelölt érték vátozását jelző esemény paramétere.
        /// </summary>
        public class OLSelectionChangedEventArgs : EventArgs
        {
            /// <summary>
            /// Az új érték.
            /// </summary>
            public OptionListItem SelectedItem { get; private set; }

            public OLSelectionChangedEventArgs( OptionListItem SelectedItem )
            {
                this.SelectedItem = SelectedItem;
            }
        }
        
        /// <summary>
        /// Esemény, amely a kiválasztott érték megváltozását jelzi.
        /// </summary>
        [Description( "A kiválasztott érték megváltozását jelző esemény." )]
        public event EventHandler<OLSelectionChangedEventArgs> SelectionChanged;



        /// <summary>
        /// A kiválasztott érték megváltozása esetén meghívnadó metódus, amely kiváltja a SelectionChanged eseményt.
        /// </summary>
        protected virtual void OnSelectionChangedEvent()
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            

            // Event will be null if there are no subscribers
            if ( EH != null )
                EH( this, new OLSelectionChangedEventArgs( this.SelectedItem ) );
            
        }

        #endregion

        #region ComboBox-os implementacio kodjai

        /// <summary>
        /// Combo box létrehozása
        /// </summary>
        private void CreateControl_Combo()
        {
            cmb_List = new ComboBox();

            foreach (var item in Items)
            {
                cmb_List.Items.Add( new ComboBoxItem
                {
                    Content = new TextBlock { Text = item.DisplayValue },
                    Tag = item
                } );
            }

            cmb_List.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler( cmb_List_SelectionChanged );

            pnl_Buttons.Children.Add( cmb_List );
        }


        void cmb_List_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            SelectedItem = SelectedItem_Combo();
        }

        private OptionListItem SelectedItem_Combo()
        {
            var cbItem = cmb_List.SelectedItem as ComboBoxItem;
            if (cbItem != null)
            {
                return cbItem.Tag as OptionListItem;
            }

            return null;
        }

        /// <summary>
        /// A SelectedValue property Set részének ComboBox-ra vonatkozó implementációja.
        /// </summary>
        /// <param name="value"></param>
        private void Set_SelectedValue_Combo( Int64? value )
        {
            CheckIfNewValueIsInList( value );

            // értékhez tartozó elem
            var selItem = cmb_List.Items.Cast<ComboBoxItem>().Where(C => (C.Tag as OptionListItem).Value == value).SingleOrDefault();

            // beállítjuk a kiválasztást
            cmb_List.SelectedItem = selItem;
        }

        /// <summary>
        /// Lenyitja a ComboBox-ot - föltéve, hogy a control Combo-s megjelenítési módban van.
        /// (Ha gombos, akkor nem csinál semmit.)
        /// </summary>
        public void DropDownCombo()
        {
            if ( m_CurrentDisplayMode == DisplayModeEnum.ComboBox )
            {
                cmb_List.IsDropDownOpen = true;
            }
        }
        #endregion

        #region RadioButton-os megejelnites kodjai

        private List<ToggleButton> Buttons;
        private Boolean InternalStateChange = false;

        /// <summary>
        /// A gombok (és az őket befoglaló mindenség) létrehozása.
        /// </summary>
        private void CreateControl_Buttons()
        {
            if ( Buttons == null )
                Buttons = new List<ToggleButton>();
            else
                Buttons.Clear();

            int column = 0;
            foreach ( var OLI in Items )
            {
                // oszlop hozzá adása a gridhez, mert ez szét tudja húzni magát széltében, ahány oszlop annyira osztja
                var col = new ColumnDefinition();
                pnl_Buttons.ColumnDefinitions.Add( col) ;

                ToggleButton B = new ToggleButton()
                {
                    Tag = OLI,
                    Style = OptionListButtonStyle
                };

                //Kep nincs, a szoveg kerul a gombra.

                #region Eredeti megoldas. Egy soros megjelenites, a hosszu szovegek nem latszodnak.
                //B.Content = OLI.DisplayValue;
                #endregion

                var TB = new TextBlock()
                {
                    Text = OLI.DisplayValue,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    LineHeight = FontSize + 4,
                    LineStackingStrategy = LineStackingStrategy.BlockLineHeight
                };

                B.Content = TB;
                    
                // grid melyik oszlopába kerüljön
                B.SetValue( Grid.ColumnProperty, column++ );
                B.Checked   += B_Checked;
                B.Unchecked += B_UnChecked;            
                                
                // felvétel
                Buttons.Add( B );
                
                // hozzáadjuk a gridhez
                pnl_Buttons.Children.Add( B );
                                
            }

        }

        void B_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            var senderButton = sender as ToggleButton;

            if (IsAlreadySelected(senderButton))
            {
                // (Nem hagyjuk kikapcsolni a mar kivalaszott gomb bekapcsolt allapotat.)
                e.Handled = true;
                return;
            }

            ChangeStateIfNoChangeInProgress(() => HandleButtonCheck(senderButton));
        }

        private bool IsAlreadySelected(ToggleButton SenderButton)
        {
            return m_SelectedItem == GetOptionListItemFromButton(SenderButton);
        }

        private void HandleButtonCheck(ToggleButton senderButton)
        {
            UncheckOtherButtons(senderButton);
            SelectedItem = GetOptionListItemFromButton(senderButton);

            // ez egy hack. A ToggleButtonon rajta marad a kék Focus keret, ha így kapcsoljuk ki. moveFocus párja
            senderButton.Focus();
        }

        private static OptionListItem GetOptionListItemFromButton(ToggleButton senderButton)
        {
            return (OptionListItem)senderButton.Tag;
        }

        private void UncheckOtherButtons(ToggleButton senderButton)
        {
            Buttons.ForEach(LB =>
            {
                if (LB != senderButton)
                {
                    LB.IsChecked = false;
                    // ez egy hack. A ToggleButtonon rajta marad a kék Focus keret, ha így kapcsoljuk ki. Ehhez kell a végén B.Focus
                    LB.MoveFocus(new System.Windows.Input.TraversalRequest(System.Windows.Input.FocusNavigationDirection.First));
                }
            });
        }

        void B_UnChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeStateIfNoChangeInProgress(() => HandleButtonUncheck(sender as ToggleButton));
        }

        private void HandleButtonUncheck(ToggleButton B)
        {
            if (IsAlreadySelected(B) )
            {
                // (Nem hagyjuk kikapcsolni a mar kivalaszott gomb bekapcsolt allapotat.)
                B.IsChecked = true;
            }
            else
            {
                // különben levesszük a kijelölést
                SelectedItem = null;
            }
        }

        /// <summary>
        /// A SelectedValue property Set részének ComboBox-ra vonatkozó implementációja.
        /// </summary>
        /// <param name="value"></param>
        private void Set_SelectedValue_Buttons( Int64? value )
        {
            ChangeStateIfNoChangeInProgress(() => SelectItemByValue(value));
        }

        private void SelectItemByValue(Int64? value)
        {
            UncheckAllButtons();
            if (value != null)
            {
                CheckIfNewValueIsInList(value);
                var selectedButton = GetButtonByValue(value);
                selectedButton.IsChecked = true;
                SelectedItem = GetOptionListItemFromButton(selectedButton);
            }
        }

        private ToggleButton GetButtonByValue(Int64? value)
        {
            return Buttons.FirstOrDefault(B => GetOptionListItemFromButton(B).Value == value);
        }

        private void UncheckAllButtons()
        {
            if ( Buttons != null )
                Buttons.ForEach(B => { B.IsChecked = false; });
        }

        private void ChangeStateIfNoChangeInProgress(Action StateChangeAction)
        {
            if (InternalStateChange == false)
            {
                InternalStateChange = true;
                try
                {
                    StateChangeAction();
                }
                finally
                {
                    InternalStateChange = false;
                }
            }
        }

        #endregion

        /// <summary>
        /// The <see cref="Items" /> dependency property's name.
        /// </summary>
        public const string ItemsPropertyName = "Items";

        /// <summary>
        /// Gets or sets the value of the <see cref="Items" />
        /// property. This is a dependency property.
        /// </summary>
        public KodLista Items
        {
            get
            {
                return (KodLista)GetValue( ItemsProperty );
            }
            set
            {
                SetValue( ItemsProperty, value.OrderBy( I => I.DisplayValue ).ToList() );
            }
        }

        /// <summary>
        /// Identifies the <see cref="Items" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            ItemsPropertyName,
            typeof( KodLista ),
            typeof( OptionList ),
            new UIPropertyMetadata( new List<OptionListItem>(),
                new PropertyChangedCallback( OnItemsChanged ) ) );

        private static void OnItemsChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            OptionList optionList = d as OptionList;
            if ( optionList != null )
                //Eldontjuk, hogy mivel jelenitjuk meg az ertekeket.
                optionList.Set_CurrentDisplayMode();

        }


        /// <summary>
        /// The <see cref="SelectedValue" /> dependency property's name.
        /// </summary>
        public const string SelectedValuePropertyName = "SelectedValue";

        /// <summary>
        /// Gets or sets the value of the <see cref="SelectedValue" />
        /// property. This is a dependency property.
        /// </summary>
        public Int64? SelectedValue
        {
            get
            {
                return (Int64?)GetValue( SelectedValueProperty );
            }
            set
            {
                SetValue( SelectedValueProperty, value );
            }
        }

        /// <summary>
        /// Identifies the <see cref="SelectedValue" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register(
            SelectedValuePropertyName,
            typeof( Int64? ),
            typeof( OptionList ),
            new UIPropertyMetadata( new Int64() ,
                new PropertyChangedCallback( OnSelectedValueChanged ) ) );

        private static void OnSelectedValueChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            OptionList optionList = d as OptionList;
            if ( optionList != null )
            {
                switch ( optionList.CurrentDisplayMode )
                {
                    case DisplayModeEnum.RadioButtons:
                        optionList.Set_SelectedValue_Buttons( (Int64?)e.NewValue );
                        break;
                    case DisplayModeEnum.ComboBox:
                        optionList.Set_SelectedValue_Combo( (Int64?)e.NewValue );
                        break;
                    default:
                        optionList.Set_SelectedValue_Combo( (Int64?)e.NewValue );
                        break;
                }
            }

        }

        #region OptionListButtonStyle
        public Style OptionListButtonStyle
        {
            get
            {
                // meg nem kerestük meg?
                if ( m_OptionListButtonStyle == null )
                {
                    // megkeressük, hogy futás során később már ne kelljen
                    m_OptionListButtonStyle = FindResource( "OptionListButtonStyle" ) as Style;
                }

                return m_OptionListButtonStyle;
            }
        }
        static private Style m_OptionListButtonStyle;
        #endregion

    }
}
