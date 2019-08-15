using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Mini.Models;
using Android.Views.InputMethods;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Mini
{
    [Activity(Label = "Mini", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, Java.IO.ISerializable
    {

        #region private variables

        private Complex _temp1 = new Complex();

        private List<object> _items = new List<object>();

        private string inputItemText;

        private double tryParse;

        private EditText QText;

        private EditText AText;

        private RadioButton Degree;

        private RadioButton Radient;

        private RadioButton Gradient;

        private BaseModel _baseModel = new BaseModel();

        private List<Complex> _comp = new List<Complex>();

        AlertDialog alertDialog;

        private Mode _solve = new Mode();

        #region others

        EditText ConstantTextbox;

        EditText First_AP;

        EditText Diff_AP;

        EditText Last_AP;

        EditText First_GP;

        EditText Rat_GP;

        EditText Last_GP;

        EditText First_HP;

        EditText Diff_HP;

        EditText Last_HP;

        EditText TwoA1;

        EditText TwoA2;

        EditText TwoA;

        EditText TwoB1;

        EditText TwoB2;

        EditText TwoB;

        EditText ThreeA1;

        EditText ThreeA2;

        EditText ThreeA3;

        EditText ThreeA;

        EditText ThreeB1;

        EditText ThreeB2;

        EditText ThreeB3;

        EditText ThreeB;

        EditText ThreeC1;

        EditText ThreeC2;

        EditText ThreeC3;

        EditText ThreeC;

        EditText Conv_From;

        TextView Conv_To;

        TextView ToConstant;

        TextView AnsAp;

        TextView AnsGp;

        TextView AnsHp;

        TextView AnsTwoX;

        TextView AnsTwoY;

        TextView AnsThreeX;

        TextView AnsThreeY;

        TextView AnsThreeZ;

        Spinner ConstantSpinner;

        Spinner APSpinner;

        Spinner GPSpinner;

        Spinner HPSpinner;

        Spinner ConvSpinner;

        Spinner FromSpinner;

        Spinner ToSpinner;

        #endregion

        #region List

        private Spinner Mat_StatSpinner;

        private Spinner A_FSpinner;

        private EditText MatrixM;

        private EditText MatrixN;

        private EditText StatCount;

        private Button BackFromList;

        private Button NextFromList;

        private LinearLayout IsMat, IsStat;

        #endregion

        #region ListItems

        LinearLayout ListItemsGrid;

        Button DoneFromListItems;

        Button BackFromOthers;

        Button APSolve;

        Button GPSolve;

        Button HPSolve;

        Button TwoSolve;

        Button ThreeSolve;

        #endregion

        #endregion

        #region public constructor

        public MainActivity()
        {

        }

        #endregion

        #region public properties

        public Mode Solve
        {
            get
            {
                return _solve;
            }
            set
            {
                _solve = value;
            }
        }

        public BaseModel BaseModel
        {
            get
            {
                return _baseModel;
            }
            set
            {
                _baseModel = value;
            }
        }

        #endregion

        #region private methods

        private void OthersView()
        {
            BaseModel.IsOthers = true;
            SetContentView(Resource.Layout.Others);
            Solve = Mode.Degree;
            #region assign Others

            #region Textbox

            ConstantTextbox = (EditText)FindViewById(Resource.Id.Q_Constant);

            First_AP = (EditText)FindViewById(Resource.Id.First_AP);

            Diff_AP = (EditText)FindViewById(Resource.Id.Diff_AP);

            Last_AP = (EditText)FindViewById(Resource.Id.Last_AP);

            First_GP = (EditText)FindViewById(Resource.Id.First_GP);

            Rat_GP = (EditText)FindViewById(Resource.Id.Rat_GP);

            Last_GP = (EditText)FindViewById(Resource.Id.Last_GP);

            First_HP = (EditText)FindViewById(Resource.Id.First_HP);

            Diff_HP = (EditText)FindViewById(Resource.Id.Diff_HP);

            Last_HP = (EditText)FindViewById(Resource.Id.Last_HP);

            TwoA1 = (EditText)FindViewById(Resource.Id.TwoA1);

            TwoA2 = (EditText)FindViewById(Resource.Id.TwoA2);

            TwoA = (EditText)FindViewById(Resource.Id.TwoA);

            TwoB1 = (EditText)FindViewById(Resource.Id.TwoB1);

            TwoB2 = (EditText)FindViewById(Resource.Id.TwoB2);

            TwoB = (EditText)FindViewById(Resource.Id.TwoB);

            ThreeA1 = (EditText)FindViewById(Resource.Id.ThreeA1);

            ThreeA2 = (EditText)FindViewById(Resource.Id.ThreeA2);

            ThreeA3 = (EditText)FindViewById(Resource.Id.ThreeA3);

            ThreeA = (EditText)FindViewById(Resource.Id.ThreeA);

            ThreeB1 = (EditText)FindViewById(Resource.Id.ThreeB1);

            ThreeB2 = (EditText)FindViewById(Resource.Id.ThreeB2);

            ThreeB3 = (EditText)FindViewById(Resource.Id.ThreeB3);

            ThreeB = (EditText)FindViewById(Resource.Id.ThreeB);

            ThreeC1 = (EditText)FindViewById(Resource.Id.ThreeC1);

            ThreeC2 = (EditText)FindViewById(Resource.Id.ThreeC2);

            ThreeC3 = (EditText)FindViewById(Resource.Id.ThreeC3);

            ThreeC = (EditText)FindViewById(Resource.Id.ThreeC);

            Conv_From = (EditText)FindViewById(Resource.Id.Conv_From);

            #endregion

            #region Button

            BackFromOthers = (Button)FindViewById(Resource.Id.BackFromOthers);

            APSolve = (Button)FindViewById(Resource.Id.APSolve);

            GPSolve = (Button)FindViewById(Resource.Id.GPSolve);

            HPSolve = (Button)FindViewById(Resource.Id.HPSolve);

            TwoSolve = (Button)FindViewById(Resource.Id.TwoSolve);

            ThreeSolve = (Button)FindViewById(Resource.Id.ThreeSolve);

            #endregion

            #region Label

            Conv_To = (TextView)FindViewById(Resource.Id.Conv_To);

            ToConstant = (TextView)FindViewById(Resource.Id.ToConstant);

            AnsAp = (TextView)FindViewById(Resource.Id.AnsAp);

            AnsGp = (TextView)FindViewById(Resource.Id.AnsGp);

            AnsHp = (TextView)FindViewById(Resource.Id.AnsHp);

            AnsTwoX = (TextView)FindViewById(Resource.Id.AnsTwoX);

            AnsTwoY = (TextView)FindViewById(Resource.Id.AnsTwoY);

            AnsThreeX = (TextView)FindViewById(Resource.Id.AnsThreeX);

            AnsThreeY = (TextView)FindViewById(Resource.Id.AnsThreeY);

            AnsThreeZ = (TextView)FindViewById(Resource.Id.AnsThreeZ);

            #endregion

            #region Spinner

            ConstantSpinner = (Spinner)FindViewById(Resource.Id.ConstantSpinner);

            APSpinner = (Spinner)FindViewById(Resource.Id.APSpinner);

            GPSpinner = (Spinner)FindViewById(Resource.Id.GPSpinner);

            HPSpinner = (Spinner)FindViewById(Resource.Id.HPSpinner);

            ConvSpinner = (Spinner)FindViewById(Resource.Id.ConvSpinner);

            FromSpinner = (Spinner)FindViewById(Resource.Id.FromSpinner);

            ToSpinner = (Spinner)FindViewById(Resource.Id.ToSpinner);

            #endregion

            #endregion

            #region declare Others

            #region Textbox

            ConstantTextbox.Text = "0";

            ConstantTextbox.TextChanged += Others_TextBox_TextChanged;

            Conv_From.Text = "0";

            Conv_From.TextChanged += Others_TextBox_TextChanged;

            #endregion

            #region Button

            BackFromOthers.Click += Others_Button_Click;

            APSolve.Click += Others_Button_Click;

            GPSolve.Click += Others_Button_Click;

            HPSolve.Click += Others_Button_Click;

            TwoSolve.Click += Others_Button_Click;

            ThreeSolve.Click += Others_Button_Click;

            #endregion

            #region Label

            Conv_To = (TextView)FindViewById(Resource.Id.Conv_To);

            ToConstant.Text = "0";

            AnsAp = (TextView)FindViewById(Resource.Id.AnsAp);

            AnsGp = (TextView)FindViewById(Resource.Id.AnsGp);

            AnsHp = (TextView)FindViewById(Resource.Id.AnsHp);

            AnsTwoX = (TextView)FindViewById(Resource.Id.AnsTwoX);

            AnsTwoY = (TextView)FindViewById(Resource.Id.AnsTwoY);

            AnsThreeX = (TextView)FindViewById(Resource.Id.AnsThreeX);

            AnsThreeY = (TextView)FindViewById(Resource.Id.AnsThreeY);

            AnsThreeZ = (TextView)FindViewById(Resource.Id.AnsThreeZ);

            #endregion

            #region Spinner

            SetAdaptor(ConstantSpinner, BaseModel.Items[SpinnerItems.Constants]);

            SetAdaptor(APSpinner, BaseModel.Items[SpinnerItems.AP]);

            SetAdaptor(GPSpinner, BaseModel.Items[SpinnerItems.GP]);

            SetAdaptor(HPSpinner, BaseModel.Items[SpinnerItems.HP]);

            SetAdaptor(ConvSpinner, BaseModel.Items[SpinnerItems.Conv]);

            SetAdaptor(FromSpinner, BaseModel.Items[SpinnerItems.Conv_From]);

            SetAdaptor(ToSpinner, BaseModel.Items[SpinnerItems.Conv_To]);

            ConstantSpinner.SetSelection(0);

            ConstantSpinner.ItemSelected += Others_Spinner_ItemSelected;

            ConvSpinner.ItemSelected += Others_Spinner_ItemSelected;

            FromSpinner.ItemSelected += Others_Spinner_ItemSelected;

            ToSpinner.ItemSelected += Others_Spinner_ItemSelected;

            #endregion

            #endregion

        }

        private void MainView()
        {
            BaseModel.IsMain = true;

            SetContentView(Resource.Layout.Main);

            #region declaring MainWindow

            #region TextBox

            QText = (EditText)FindViewById(Resource.Id.QText);

            AText = (EditText)FindViewById(Resource.Id.QText);

            #endregion

            #region RadioButtons

            Degree = (RadioButton)FindViewById(Resource.Id.Degree);

            Radient = (RadioButton)FindViewById(Resource.Id.Radient);

            Gradient = (RadioButton)FindViewById(Resource.Id.Gradient);

            #endregion

            #region Buttons

            Button BackSpace = (Button)FindViewById(Resource.Id.BackSpace);

            Button Up = (Button)FindViewById(Resource.Id.Up);

            Button Down = (Button)FindViewById(Resource.Id.Down);

            Button Others = (Button)FindViewById(Resource.Id.Others);

            Button AND = (Button)FindViewById(Resource.Id.AND);

            Button XOR = (Button)FindViewById(Resource.Id.XOR);

            Button A = (Button)FindViewById(Resource.Id.A);

            Button B = (Button)FindViewById(Resource.Id.B);

            Button C = (Button)FindViewById(Resource.Id.C);

            Button Inverse = (Button)FindViewById(Resource.Id.Inv);

            Button SquareRoot = (Button)FindViewById(Resource.Id.SQRT);

            Button CubeRoot = (Button)FindViewById(Resource.Id.CBRT);

            Button NRoot = (Button)FindViewById(Resource.Id.NRT);

            Button Log = (Button)FindViewById(Resource.Id.LOG);

            Button Ln = (Button)FindViewById(Resource.Id.LN);

            Button Permutation = (Button)FindViewById(Resource.Id.Permutation);

            Button Rect = (Button)FindViewById(Resource.Id.RAngle);

            Button Pol = (Button)FindViewById(Resource.Id.APolar);

            Button i = (Button)FindViewById(Resource.Id.i);

            Button Lcm = (Button)FindViewById(Resource.Id.LCM);

            Button Abs = (Button)FindViewById(Resource.Id.Abs);

            Button Sin = (Button)FindViewById(Resource.Id.Sin);

            Button Cos = (Button)FindViewById(Resource.Id.Cos);

            Button Tan = (Button)FindViewById(Resource.Id.Tan);

            Button Sinh = (Button)FindViewById(Resource.Id.Sinh);

            Button Cosh = (Button)FindViewById(Resource.Id.Cosh);

            Button Tanh = (Button)FindViewById(Resource.Id.Tanh);

            Button Matrix = (Button)FindViewById(Resource.Id.Mat);

            Button StandardDeviation = (Button)FindViewById(Resource.Id.SD);

            Button Variance = (Button)FindViewById(Resource.Id.Var);

            Button Clear = (Button)FindViewById(Resource.Id.Clr);

            Button Prime = (Button)FindViewById(Resource.Id.Prime);

            Button ListOfPrime = (Button)FindViewById(Resource.Id.ListOfPrime);

            Button Angle = (Button)FindViewById(Resource.Id.Angle);

            Button Pi = (Button)FindViewById(Resource.Id.Pi);

            Button Comma = (Button)FindViewById(Resource.Id.Comma);

            Button Percent = (Button)FindViewById(Resource.Id.Percent);

            Button Seven = (Button)FindViewById(Resource.Id.Seven);

            Button Eight = (Button)FindViewById(Resource.Id.Eight);

            Button Nine = (Button)FindViewById(Resource.Id.Nine);

            Button Mean = (Button)FindViewById(Resource.Id.XBar);

            Button MeanSquare = (Button)FindViewById(Resource.Id.XBarSqr);

            Button Sum = (Button)FindViewById(Resource.Id.Sum);

            Button Four = (Button)FindViewById(Resource.Id.Four);

            Button Five = (Button)FindViewById(Resource.Id.Five);

            Button Six = (Button)FindViewById(Resource.Id.Six);

            Button Sub = (Button)FindViewById(Resource.Id.Minus);

            Button Add = (Button)FindViewById(Resource.Id.Add);

            Button SumOfSquare = (Button)FindViewById(Resource.Id.SumOfSquare);

            Button One = (Button)FindViewById(Resource.Id.One);

            Button Two = (Button)FindViewById(Resource.Id.Two);

            Button Three = (Button)FindViewById(Resource.Id.Three);

            Button Zero = (Button)FindViewById(Resource.Id.Zero);

            Button Decimal = (Button)FindViewById(Resource.Id.Decimal);

            Button Solve = (Button)FindViewById(Resource.Id.Solve);

            #endregion

            #endregion

            #region assign MainWindow

            #region TextBox

            QText.SetRawInputType(Android.Text.InputTypes.ClassText);

            QText.SetTextIsSelectable(true);

            QText.Text = BaseModel.QText;

            Window.SetSoftInputMode(SoftInput.StateHidden);

            #endregion

            #region RadioButton

            Degree.Checked = BaseModel.IsDegree;

            Radient.Checked = BaseModel.IsRadient;

            Gradient.Checked = BaseModel.IsGradient;

            Degree.CheckedChange += Main_CheckedChange;

            Radient.CheckedChange += Main_CheckedChange;

            Gradient.CheckedChange += Main_CheckedChange;

            #endregion

            #region Button

            #region Click

            BackSpace.Click += Main_Button_Click;

            Down.Click += Main_Button_Click;

            Up.Click += Main_Button_Click;

            AND.Click += Main_Button_Click;

            XOR.Click += Main_Button_Click;

            A.Click += Main_Button_Click;

            B.Click += Main_Button_Click;

            C.Click += Main_Button_Click;

            Inverse.Click += Main_Button_Click;

            SquareRoot.Click += Main_Button_Click;

            CubeRoot.Click += Main_Button_Click;

            NRoot.Click += Main_Button_Click;

            Log.Click += Main_Button_Click;

            Ln.Click += Main_Button_Click;

            Permutation.Click += Main_Button_Click;

            Pol.Click += Main_Button_Click;

            Rect.Click += Main_Button_Click;

            i.Click += Main_Button_Click;

            Lcm.Click += Main_Button_Click;

            Abs.Click += Main_Button_Click;

            Sin.Click += Main_Button_Click;

            Cos.Click += Main_Button_Click;

            Tan.Click += Main_Button_Click;

            Sinh.Click += Main_Button_Click;

            Cosh.Click += Main_Button_Click;

            Tanh.Click += Main_Button_Click;

            Matrix.Click += Main_Button_Click;

            StandardDeviation.Click += Main_Button_Click;

            Variance.Click += Main_Button_Click;

            Clear.Click += Main_Button_Click;

            Prime.Click += Main_Button_Click;

            ListOfPrime.Click += Main_Button_Click;

            Angle.Click += Main_Button_Click;

            Pi.Click += Main_Button_Click;

            Comma.Click += Main_Button_Click;

            Percent.Click += Main_Button_Click;

            Seven.Click += Main_Button_Click;

            Eight.Click += Main_Button_Click;

            Nine.Click += Main_Button_Click;

            Mean.Click += Main_Button_Click;

            MeanSquare.Click += Main_Button_Click;

            Sum.Click += Main_Button_Click;

            Four.Click += Main_Button_Click;

            Five.Click += Main_Button_Click;

            Six.Click += Main_Button_Click;

            Sub.Click += Main_Button_Click;

            Add.Click += Main_Button_Click;

            SumOfSquare.Click += Main_Button_Click;

            One.Click += Main_Button_Click;

            Two.Click += Main_Button_Click;

            Three.Click += Main_Button_Click;

            Zero.Click += Main_Button_Click;

            Decimal.Click += Main_Button_Click;

            Solve.Click += Main_Button_Click;

            Others.Click += Main_Button_Click;

            #endregion

            #region LongClick

            AND.LongClick += Main_Button_LongClick;

            XOR.LongClick += Main_Button_LongClick;

            A.LongClick += Main_Button_LongClick;

            B.LongClick += Main_Button_LongClick;

            C.LongClick += Main_Button_LongClick;

            Inverse.LongClick += Main_Button_LongClick;

            SquareRoot.LongClick += Main_Button_LongClick;

            CubeRoot.LongClick += Main_Button_LongClick;

            NRoot.LongClick += Main_Button_LongClick;

            Log.LongClick += Main_Button_LongClick;

            Ln.LongClick += Main_Button_LongClick;

            Permutation.LongClick += Main_Button_LongClick;

            Pol.LongClick += Main_Button_LongClick;

            Rect.LongClick += Main_Button_LongClick;

            i.LongClick += Main_Button_LongClick;

            Lcm.LongClick += Main_Button_LongClick;

            Abs.LongClick += Main_Button_LongClick;

            Sin.LongClick += Main_Button_LongClick;

            Cos.LongClick += Main_Button_LongClick;

            Tan.LongClick += Main_Button_LongClick;

            Sinh.LongClick += Main_Button_LongClick;

            Cosh.LongClick += Main_Button_LongClick;

            Tanh.LongClick += Main_Button_LongClick;

            Matrix.LongClick += Main_Button_LongClick;

            StandardDeviation.LongClick += Main_Button_LongClick;

            Variance.LongClick += Main_Button_LongClick;

            Seven.LongClick += Main_Button_LongClick;

            Eight.LongClick += Main_Button_LongClick;

            Nine.LongClick += Main_Button_LongClick;

            Mean.LongClick += Main_Button_LongClick;

            MeanSquare.LongClick += Main_Button_LongClick;

            Sum.LongClick += Main_Button_LongClick;

            Four.LongClick += Main_Button_LongClick;

            Five.LongClick += Main_Button_LongClick;

            Six.LongClick += Main_Button_LongClick;

            Sub.LongClick += Main_Button_LongClick;

            Add.LongClick += Main_Button_LongClick;

            One.LongClick += Main_Button_LongClick;

            Two.LongClick += Main_Button_LongClick;

            Three.LongClick += Main_Button_LongClick;

            #endregion

            #endregion

            #endregion
        }

        private void ListView()
        {
            BaseModel.IsList = true;

            SetContentView(Resource.Layout.List);

            #region declare List

            #region Layout

            IsMat = (LinearLayout)FindViewById(Resource.Id.IsMat);

            IsStat = (LinearLayout)FindViewById(Resource.Id.IsStat);

            #endregion

            #region Spinner

            Mat_StatSpinner = (Spinner)FindViewById(Resource.Id.MatrixOrStatSpinner);

            A_FSpinner = (Spinner)FindViewById(Resource.Id.AtoF);

            #endregion

            #region Textbox

            MatrixM = (EditText)FindViewById(Resource.Id.MatrixM);

            MatrixN = (EditText)FindViewById(Resource.Id.MatrixN);

            StatCount = (EditText)FindViewById(Resource.Id.StatCount);

            #endregion

            #region Button

            BackFromList = (Button)FindViewById(Resource.Id.BackFromList);

            NextFromList = (Button)FindViewById(Resource.Id.NextFromList);

            #endregion

            #endregion

            #region assign List

            #region Spinner

            SetAdaptor(Mat_StatSpinner, BaseModel.Items[SpinnerItems.StatMat]);

            SetAdaptor(A_FSpinner, BaseModel.Items[SpinnerItems.A_F]);

            Mat_StatSpinner.SetSelection(BaseModel.KeyIndex);

            A_FSpinner.SetSelection(BaseModel.ValueIndex);

            Mat_StatSpinner.ItemSelected += List_Spinner_ItemSelected;

            A_FSpinner.ItemSelected += List_Spinner_ItemSelected;

            #endregion

            #region Textbox

            MatrixM.Text = BaseModel.MatrixM;

            MatrixN.Text = BaseModel.MatrixN;

            StatCount.Text = BaseModel.StatCount;

            MatrixM.TextChanged += List_TextBox_TextChanged;

            MatrixN.TextChanged += List_TextBox_TextChanged;

            StatCount.TextChanged += List_TextBox_TextChanged;

            #endregion

            #region Button

            SetVisible();

            BackFromList.Click += List_Button_Click;

            NextFromList.Click += List_Button_Click;

            #endregion

            #endregion

        }

        private void ListItemsView()
        {
            BaseModel.IsListItems = true;
            SetContentView(Resource.Layout.ListItems);

            ListItemsGrid = (LinearLayout)FindViewById(Resource.Id.ListItemsGrid);
            if (BaseModel.KeyIndex == 0)
            {
                BaseModel.TempMatList = BaseModel.MatLists[BaseModel.ValueIndex];
                if (BaseModel.TempMatList.GetLength(0) != Convert.ToInt32(BaseModel.MatrixM) || BaseModel.TempMatList.GetLength(1) != Convert.ToInt32(BaseModel.MatrixN))
                {
                    BaseModel.TempMatList = new double[Convert.ToInt32(BaseModel.MatrixM), Convert.ToInt32(BaseModel.MatrixN)];
                }
                InsertChildToGrid(Convert.ToInt32(BaseModel.MatrixM), Convert.ToInt32(BaseModel.MatrixN));
            }
            else
            {
                BaseModel.TempStatList = BaseModel.StatLists[BaseModel.ValueIndex];
                if (BaseModel.TempStatList.Count != Convert.ToInt32(BaseModel.StatCount))
                {
                    BaseModel.TempStatList = new List<double>(new double[Convert.ToInt32(BaseModel.StatCount)]);
                }
                InsertChildToGrid(Convert.ToInt32(BaseModel.StatCount), 0);
            }
        }

        private void InsertChildToGrid(int m, int n = 0)
        {
            ListItemsGrid.RemoveAllViews();
            for (int i = 1; i <= m; i++)
            {
                if (n > 0)
                {
                    for (int j = 1; j <= n; j++)
                    {
                        LinearLayout layoutRow = new LinearLayout(this) { LayoutParameters = new Android.Widget.LinearLayout.LayoutParams(Android.Widget.LinearLayout.LayoutParams.MatchParent, 0, 0.3f) };
                        layoutRow.SetPadding(2, 2, 2, 2);
                        TextView innerText = new TextView(this) { Gravity = GravityFlags.CenterVertical, TextSize = 18, LayoutParameters = new Android.Widget.LinearLayout.LayoutParams(Android.Widget.LinearLayout.LayoutParams.WrapContent, Android.Widget.LinearLayout.LayoutParams.WrapContent), Id = i - 1 };
                        EditText editItem = new EditText(this) { LayoutParameters = new Android.Widget.LinearLayout.LayoutParams(0, Android.Widget.LinearLayout.LayoutParams.WrapContent, 0.3f), Text = Convert.ToString(BaseModel.TempMatList[i - 1, j - 1]), Id = j - 1, InputType = Android.Text.InputTypes.NumberFlagDecimal | Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagSigned };

                        editItem.SetSingleLine(true);
                        editItem.TextChanged += ListItems_TextBox_TextChanged;
                        innerText.Text = "a" + i + j + ": ";
                        layoutRow.AddView(innerText);
                        layoutRow.AddView(editItem);
                        ListItemsGrid.AddView(layoutRow);
                    }
                }
                else
                {
                    LinearLayout layoutRow = new LinearLayout(this) { LayoutParameters = new Android.Widget.LinearLayout.LayoutParams(Android.Widget.LinearLayout.LayoutParams.MatchParent, 0, 0.3f) };
                    layoutRow.SetPadding(2, 2, 2, 2);
                    TextView innerText = new TextView(this) { Gravity = GravityFlags.CenterVertical, TextSize = 18, LayoutParameters = new Android.Widget.LinearLayout.LayoutParams(Android.Widget.LinearLayout.LayoutParams.WrapContent, Android.Widget.LinearLayout.LayoutParams.WrapContent) };
                    EditText editItem = new EditText(this) { LayoutParameters = new Android.Widget.LinearLayout.LayoutParams(0, Android.Widget.LinearLayout.LayoutParams.WrapContent, 0.3f), Text = Convert.ToString(BaseModel.TempStatList[i - 1]), Id = i - 1, InputType = Android.Text.InputTypes.NumberFlagDecimal | Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagSigned };
                    editItem.SetSingleLine(true);
                    editItem.TextChanged += ListItems_TextBox_TextChanged;
                    innerText.Text = "a" + i + ": ";
                    layoutRow.AddView(innerText);
                    layoutRow.AddView(editItem);
                    ListItemsGrid.AddView(layoutRow);
                }
            }
            LinearLayout layoutButton = new LinearLayout(this) { LayoutParameters = new Android.Widget.LinearLayout.LayoutParams(Android.Widget.LinearLayout.LayoutParams.MatchParent, 0, 0.3f) };
            layoutButton.SetPadding(2, 2, 2, 2);
            Button BackFromListItems = new Button(this) { LayoutParameters = new Android.Widget.LinearLayout.LayoutParams(0, Android.Widget.LinearLayout.LayoutParams.WrapContent, 0.3f), TextSize = 18, Text = "Back" };
            DoneFromListItems = new Button(this) { LayoutParameters = new Android.Widget.LinearLayout.LayoutParams(0, Android.Widget.LinearLayout.LayoutParams.WrapContent, 0.3f), TextSize = 18, Text = "Done" };
            Button CloseFromListItems = new Button(this) { LayoutParameters = new Android.Widget.LinearLayout.LayoutParams(0, Android.Widget.LinearLayout.LayoutParams.WrapContent, 0.3f), TextSize = 18, Text = "Close" };
            BackFromListItems.Click += ListItems_Button_Click;
            DoneFromListItems.Click += ListItems_Button_Click;
            CloseFromListItems.Click += ListItems_Button_Click;
            layoutButton.AddView(BackFromListItems);
            layoutButton.AddView(DoneFromListItems);
            layoutButton.AddView(CloseFromListItems);
            ListItemsGrid.AddView(layoutButton);
        }

        private void SetAdaptor(Spinner spinner, string[] items)
        {
            BaseModel.ArrayAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, items);
            spinner.Adapter = BaseModel.ArrayAdapter;
        }

        private void SetContent(int index, string content)
        {
            BaseModel.QText = QText.Text ?? "";
            BaseModel.QText = BaseModel.QText.Substring(0, index) + content + BaseModel.Temp + BaseModel.QText.Substring(index);
            QText.Text = BaseModel.QText;
            BaseModel.StartIndex = index + (BaseModel.Content ?? "").Length;
            QText.SetSelection(BaseModel.StartIndex);
        }

        private void SetVisible()
        {
            if (Mat_StatSpinner.SelectedItemPosition == 0)
            {
                if (MatrixM.Text.Length == 0 || MatrixN.Text.Length == 0 || Convert.ToInt32(MatrixM.Text) <= 0 || Convert.ToInt32(MatrixN.Text) <= 0)
                {
                    NextFromList.Enabled = false;
                }
                else
                {
                    NextFromList.Enabled = true;
                }
            }
            else
            {
                if (StatCount.Text.Length == 0 || Convert.ToInt32(StatCount.Text) <= 0)
                {
                    NextFromList.Enabled = false;
                }
                else
                {
                    NextFromList.Enabled = true;
                }
            }
        }

        private void SetToConstant()
        {
            try
            {
                ToConstant.Text = (System.Convert.ToDouble(ConstantTextbox.Text) * BaseModel.MathFunctions.Values(ConstantSpinner.SelectedItemPosition)) + "  " + BaseModel.MathFunctions.Units(ConstantSpinner.SelectedItemPosition);
            }
            catch (Exception)
            {
                ToConstant.Text = "0  " + BaseModel.MathFunctions.Units(ConstantSpinner.SelectedItemPosition);
            }
        }

        private void AP_GP_HP(string type)
        {
            try
            {
                if (type == "AP")
                {
                    if (APSpinner.SelectedItemPosition == 0)
                    {
                        AnsAp.Text = BaseModel.MathFunctions.APSum(Convert.ToDouble(First_AP.Text), Convert.ToDouble(Diff_AP.Text), Convert.ToDouble(Last_AP.Text)).ToString();
                    }
                    else
                    {
                        AnsAp.Text = BaseModel.MathFunctions.APDiff(Convert.ToDouble(First_AP.Text), Convert.ToDouble(Diff_AP.Text), Convert.ToDouble(Last_AP.Text)).ToString();
                    }
                }
                else if (type == "GP")
                {
                    if (GPSpinner.SelectedItemPosition == 0)
                    {
                        AnsGp.Text = BaseModel.MathFunctions.GPSum(Convert.ToDouble(First_GP.Text), Convert.ToDouble(Rat_GP.Text), Convert.ToDouble(Last_GP.Text)).ToString();
                    }
                    else
                    {
                        AnsGp.Text = BaseModel.MathFunctions.GPDiff(Convert.ToDouble(First_GP.Text), Convert.ToDouble(Rat_GP.Text), Convert.ToDouble(Last_GP.Text)).ToString();
                    }
                }
                else if (type == "HP")
                {
                    if (HPSpinner.SelectedItemPosition == 0)
                    {
                        AnsHp.Text = BaseModel.MathFunctions.HPSum(Convert.ToDouble(First_HP.Text), Convert.ToDouble(Diff_HP.Text), Convert.ToDouble(Last_HP.Text)).ToString();
                    }
                    else
                    {
                        AnsHp.Text = BaseModel.MathFunctions.HPDiff(Convert.ToDouble(First_HP.Text), Convert.ToDouble(Diff_HP.Text), Convert.ToDouble(Last_HP.Text)).ToString();
                    }
                }
            }
            catch (Exception)
            {
                Message("Invalid Input!");
            }
        }

        private void Message(string message, string type = "Error")
        {
            alertDialog = new AlertDialog.Builder(this).Create();
            alertDialog.SetTitle(type);
            alertDialog.SetMessage(message);
            alertDialog.Show();
        }

        private void GetResult()
        {
            if (!string.IsNullOrWhiteSpace(Conv_From.Text))
            {
                if (double.TryParse(Conv_From.Text, out tryParse))
                {
                    switch (BaseModel.Converter)
                    {
                        case Converter.Angle:
                            {
                                Conv_To.Text = BaseModel.MathFunctions.AngleToAngle(BaseModel.Angle1, BaseModel.Angle2, System.Convert.ToDouble(Conv_From.Text)).ToString();
                                break;
                            }
                        case Converter.Area:
                            {
                                Conv_To.Text = BaseModel.MathFunctions.AreaToArea(BaseModel.Area1, BaseModel.Area2, System.Convert.ToDouble(Conv_From.Text)).ToString();
                                break;
                            }
                        case Converter.Energy:
                            {
                                Conv_To.Text = BaseModel.MathFunctions.EnergyToEnergy(BaseModel.Energy1, BaseModel.Energy2, System.Convert.ToDouble(Conv_From.Text)).ToString();
                                break;
                            }
                        case Converter.Length:
                            {
                                Conv_To.Text = BaseModel.MathFunctions.LengthToLength(BaseModel.Length1, BaseModel.Length2, System.Convert.ToDouble(Conv_From.Text)).ToString();
                                break;
                            }
                        case Converter.Power:
                            {
                                Conv_To.Text = BaseModel.MathFunctions.PowerToPower(BaseModel.Power1, BaseModel.Power2, System.Convert.ToDouble(Conv_From.Text)).ToString();
                                break;
                            }
                        case Converter.Pressure:
                            {
                                Conv_To.Text = BaseModel.MathFunctions.PressureToPressure(BaseModel.Pressure1, BaseModel.Pressure2, System.Convert.ToDouble(Conv_From.Text)).ToString();
                                break;
                            }
                        case Converter.Temperature:
                            {
                                Conv_To.Text = BaseModel.MathFunctions.TemperatureToTemperature(BaseModel.Temperature1, BaseModel.Temperature2, System.Convert.ToDouble(Conv_From.Text)).ToString();
                                break;
                            }
                        case Converter.Time:
                            {
                                Conv_To.Text = BaseModel.MathFunctions.TimeToTime(BaseModel.Time1, BaseModel.Time2, System.Convert.ToDouble(Conv_From.Text)).ToString();
                                break;
                            }
                        case Converter.Velocity:
                            {
                                Conv_To.Text = BaseModel.MathFunctions.VelocityToVelocity(BaseModel.Velocity1, BaseModel.Velocity2, System.Convert.ToDouble(Conv_From.Text)).ToString();
                                break;
                            }
                        case Converter.Volume:
                            {
                                Conv_To.Text = BaseModel.MathFunctions.VolumeToVolume(BaseModel.Volume1, BaseModel.Volume2, System.Convert.ToDouble(Conv_From.Text)).ToString();
                                break;
                            }
                        case Converter.Weight:
                            {
                                Conv_To.Text = BaseModel.MathFunctions.WeightToWeight(BaseModel.Weight1, BaseModel.Weight2, System.Convert.ToDouble(Conv_From.Text)).ToString();
                                break;
                            }
                    }
                }
                try
                {
                    if (BaseModel.Converter == Converter.Base && !string.IsNullOrEmpty(Conv_From.Text))
                    {
                        Conv_To.Text = BaseModel.MathFunctions.BaseToBase(BaseModel.Base1, BaseModel.Base2, Conv_From.Text).ToString();
                    }
                }
                catch (Exception ex) { Message(ex.Message); }
            }
            else
            {
                if (!string.IsNullOrEmpty(Conv_From.Text))
                {
                    Message("Invalid Input!"); ;
                }
                Conv_To.Text = "0";
            }
        }

        private void Convert_SelectionChanged()
        {
            try
            {
                switch (ConvSpinner.SelectedItem.ToString())
                {
                    case "Angle":
                        {
                            BaseModel.Converter = Converter.Angle;
                            break;
                        }
                    case "Area":
                        {
                            BaseModel.Converter = Converter.Area;
                            break;
                        }
                    case "Base":
                        {
                            BaseModel.Converter = Converter.Base;
                            break;
                        }
                    case "Energy":
                        {
                            BaseModel.Converter = Converter.Energy;
                            break;
                        }
                    case "Length":
                        {
                            BaseModel.Converter = Converter.Length;
                            break;
                        }
                    case "Power":
                        {
                            BaseModel.Converter = Converter.Power;
                            break;
                        }
                    case "Pressure":
                        {
                            BaseModel.Converter = Converter.Pressure;
                            break;
                        }
                    case "Temperature":
                        {
                            BaseModel.Converter = Converter.Temperature;
                            break;
                        }
                    case "Time":
                        {
                            BaseModel.Converter = Converter.Time;
                            break;
                        }
                    case "Velocity":
                        {
                            BaseModel.Converter = Converter.Velocity;
                            break;
                        }
                    case "Volume":
                        {
                            BaseModel.Converter = Converter.Volume;
                            break;
                        }
                    case "Weight/Mass":
                        {
                            BaseModel.Converter = Converter.Weight;
                            break;
                        }
                }
                FromSpinner.SetSelection(0);
                ToSpinner.SetSelection(0);
            }
            catch (Exception) { }
        }

        private void From_SelectionChanged()
        {
            switch (FromSpinner.SelectedItem.ToString())
            {
                case "Degree": { BaseModel.Angle1 = Angle.Degree; break; }
                case "Gradian": { BaseModel.Angle1 = Angle.Gradian; break; }
                case "Radian": { BaseModel.Angle1 = Angle.Radian; break; }
                case "Acres": { BaseModel.Area1 = Area.Acres; break; }
                case "Hectares": { BaseModel.Area1 = Area.Hectares; break; }
                case "Square centimeter": { BaseModel.Area1 = Area.SquareCentimeter; break; }
                case "Square feet": { BaseModel.Area1 = Area.SquareFeet; break; }
                case "Square inch": { BaseModel.Area1 = Area.SquareInch; break; }
                case "Square kilometer": { BaseModel.Area1 = Area.SquareKilometer; break; }
                case "Square meter": { BaseModel.Area1 = Area.SquareMeter; break; }
                case "Square mile": { BaseModel.Area1 = Area.SquareMile; break; }
                case "Square millimeter": { BaseModel.Area1 = Area.SquareMillimeter; break; }
                case "Square yard": { BaseModel.Area1 = Area.SquareYard; break; }
                case "Binary": { BaseModel.Base1 = Base.Binary; break; }
                case "Decimal": { BaseModel.Base1 = Base.Decimal; break; }
                case "Hexa Decimal": { BaseModel.Base1 = Base.HexaDecimal; break; }
                case "Octal": { BaseModel.Base1 = Base.Octal; break; }
                case "British Thermal Unit": { BaseModel.Energy1 = Energy.BritishThermalUnit; break; }
                case "Calorie": { BaseModel.Energy1 = Energy.Calorie; break; }
                case "Electron-volts": { BaseModel.Energy1 = Energy.ElectronVolts; break; }
                case "Foot-pound": { BaseModel.Energy1 = Energy.FootPound; break; }
                case "Joule": { BaseModel.Energy1 = Energy.Joule; break; }
                case "Kilocalorie": { BaseModel.Energy1 = Energy.KiloCalorie; break; }
                case "Kilojoule": { BaseModel.Energy1 = Energy.KiloJoule; break; }
                case "Angstorm": { BaseModel.Length1 = Length.Angstorm; break; }
                case "Centimeter": { BaseModel.Length1 = Length.Centimeter; break; }
                case "Chain": { BaseModel.Length1 = Length.Chain; break; }
                case "Fathom": { BaseModel.Length1 = Length.Fathom; break; }
                case "Feet": { BaseModel.Length1 = Length.Feet; break; }
                case "Hand": { BaseModel.Length1 = Length.Hand; break; }
                case "Inch": { BaseModel.Length1 = Length.Inch; break; }
                case "Kilometer": { BaseModel.Length1 = Length.Kilometer; break; }
                case "Link": { BaseModel.Length1 = Length.Link; break; }
                case "Meter": { BaseModel.Length1 = Length.Meter; break; }
                case "Microns": { BaseModel.Length1 = Length.Microns; break; }
                case "Mile": { BaseModel.Length1 = Length.Mile; break; }
                case "Millimeter": { BaseModel.Length1 = Length.Millimeter; break; }
                case "Nanometer": { BaseModel.Length1 = Length.Nanometer; break; }
                case "Nautical Mile": { BaseModel.Length1 = Length.NauticalMile; break; }
                case "PICA": { BaseModel.Length1 = Length.PICA; break; }
                case "Rods": { BaseModel.Length1 = Length.Rods; break; }
                case "Span": { BaseModel.Length1 = Length.Span; break; }
                case "Yard": { BaseModel.Length1 = Length.Yard; break; }
                case "BTU/minute": { BaseModel.Power1 = Powers.BTUminute; break; }
                case "Foot-Pound/minute": { BaseModel.Power1 = Powers.FootPound; break; }
                case "Horsepower": { BaseModel.Power1 = Powers.Horsepower; break; }
                case "Kilowatt": { BaseModel.Power1 = Powers.Kilowatt; break; }
                case "Watt": { BaseModel.Power1 = Powers.Watt; break; }
                case "Atmosphere": { BaseModel.Pressure1 = Pressure.Atmosphere; break; }
                case "Bar": { BaseModel.Pressure1 = Pressure.Bar; break; }
                case "Kilo Pascal": { BaseModel.Pressure1 = Pressure.KiloPascal; break; }
                case "Millimeter of mercury": { BaseModel.Pressure1 = Pressure.MillimeterOfMercury; break; }
                case "Pascal": { BaseModel.Pressure1 = Pressure.Pascal; break; }
                case "Pound per square inch(PSI)": { BaseModel.Pressure1 = Pressure.PoundPerSquareInch; break; }
                case "Degree Celsius": { BaseModel.Temperature1 = Temperature.DegreeCelsius; break; }
                case "Degree Fahrenheit": { BaseModel.Temperature1 = Temperature.DegreeFahrenheit; break; }
                case "Kelvin": { BaseModel.Temperature1 = Temperature.Kelvin; break; }
                case "Day": { BaseModel.Time1 = Time.Day; break; }
                case "Hour": { BaseModel.Time1 = Time.Hour; break; }
                case "Microsecond": { BaseModel.Time1 = Time.MicroSecond; break; }
                case "Millisecond": { BaseModel.Time1 = Time.MilliSecond; break; }
                case "Minute": { BaseModel.Time1 = Time.Minute; break; }
                case "Second": { BaseModel.Time1 = Time.Second; break; }
                case "Week": { BaseModel.Time1 = Time.Week; break; }
                case "Centimeter per second": { BaseModel.Velocity1 = Velocity.CentimeterPerSecond; break; }
                case "Feet per second": { BaseModel.Velocity1 = Velocity.FeetPerSecond; break; }
                case "Kilometer per hour": { BaseModel.Velocity1 = Velocity.KilometerPerHour; break; }
                case "Knots": { BaseModel.Velocity1 = Velocity.Knots; break; }
                case "Mach(at std. atm)": { BaseModel.Velocity1 = Velocity.Mach; break; }
                case "Meter per second": { BaseModel.Velocity1 = Velocity.MeterPerSecond; break; }
                case "Miles per hour": { BaseModel.Velocity1 = Velocity.MilesPerHour; break; }
                case "Cubic centimeter": { BaseModel.Volume1 = Volume.CubicCentimeter; break; }
                case "Cubic feet": { BaseModel.Volume1 = Volume.CubicFeet; break; }
                case "Cubic inch": { BaseModel.Volume1 = Volume.CubicInch; break; }
                case "Cubic meter": { BaseModel.Volume1 = Volume.CubicMeter; break; }
                case "Cubic yard": { BaseModel.Volume1 = Volume.CubicYard; break; }
                case "Fluid ounce (UK)": { BaseModel.Volume1 = Volume.FluidOunceUK; break; }
                case "Fluid ounce (US)": { BaseModel.Volume1 = Volume.FluidounceUS; break; }
                case "Gallon (UK)": { BaseModel.Volume1 = Volume.GallonUK; break; }
                case "Gallon (US)": { BaseModel.Volume1 = Volume.GallonUS; break; }
                case "Liter": { BaseModel.Volume1 = Volume.Liter; break; }
                case "Pint (UK)": { BaseModel.Volume1 = Volume.PintUK; break; }
                case "Pint (US)": { BaseModel.Volume1 = Volume.PintUS; break; }
                case "Quart (UK)": { BaseModel.Volume1 = Volume.QuartUK; break; }
                case "Quart (US)": { BaseModel.Volume1 = Volume.QuartUS; break; }
                case "Carat": { BaseModel.Weight1 = Weight.Carat; break; }
                case "Centigram": { BaseModel.Weight1 = Weight.CentiGram; break; }
                case "Decigram": { BaseModel.Weight1 = Weight.DeciGram; break; }
                case "Dekagram": { BaseModel.Weight1 = Weight.DekaGram; break; }
                case "Gram": { BaseModel.Weight1 = Weight.Gram; break; }
                case "Hectogram": { BaseModel.Weight1 = Weight.HectoGram; break; }
                case "Kilogram": { BaseModel.Weight1 = Weight.KiloGram; break; }
                case "Long ton": { BaseModel.Weight1 = Weight.LongTon; break; }
                case "Milligram": { BaseModel.Weight1 = Weight.MilliGram; break; }
                case "Ounce": { BaseModel.Weight1 = Weight.Ounce; break; }
                case "Pound": { BaseModel.Weight1 = Weight.Pound; break; }
                case "Short ton": { BaseModel.Weight1 = Weight.ShortTon; break; }
                case "Stone": { BaseModel.Weight1 = Weight.Stone; break; }
                case "Tonne": { BaseModel.Weight1 = Weight.Tonne; break; }
            }
            GetResult();
        }

        private void To_SelectionChanged()
        {
            switch (ToSpinner.SelectedItem.ToString())
            {
                case "Degree": { BaseModel.Angle2 = Angle.Degree; break; }
                case "Gradian": { BaseModel.Angle2 = Angle.Gradian; break; }
                case "Radian": { BaseModel.Angle2 = Angle.Radian; break; }
                case "Acres": { BaseModel.Area2 = Area.Acres; break; }
                case "Hectares": { BaseModel.Area2 = Area.Hectares; break; }
                case "Square centimeter": { BaseModel.Area2 = Area.SquareCentimeter; break; }
                case "Square feet": { BaseModel.Area2 = Area.SquareFeet; break; }
                case "Square inch": { BaseModel.Area2 = Area.SquareInch; break; }
                case "Square kilometer": { BaseModel.Area2 = Area.SquareKilometer; break; }
                case "Square meter": { BaseModel.Area2 = Area.SquareMeter; break; }
                case "Square mile": { BaseModel.Area2 = Area.SquareMile; break; }
                case "Square millimeter": { BaseModel.Area2 = Area.SquareMillimeter; break; }
                case "Square yard": { BaseModel.Area2 = Area.SquareYard; break; }
                case "Binary": { BaseModel.Base2 = Base.Binary; break; }
                case "Decimal": { BaseModel.Base2 = Base.Decimal; break; }
                case "Hexa Decimal": { BaseModel.Base2 = Base.HexaDecimal; break; }
                case "Octal": { BaseModel.Base2 = Base.Octal; break; }
                case "British Thermal Unit": { BaseModel.Energy2 = Energy.BritishThermalUnit; break; }
                case "Calorie": { BaseModel.Energy2 = Energy.Calorie; break; }
                case "Electron-volts": { BaseModel.Energy2 = Energy.ElectronVolts; break; }
                case "Foot-pound": { BaseModel.Energy2 = Energy.FootPound; break; }
                case "Joule": { BaseModel.Energy2 = Energy.Joule; break; }
                case "Kilocalorie": { BaseModel.Energy2 = Energy.KiloCalorie; break; }
                case "Kilojoule": { BaseModel.Energy2 = Energy.KiloJoule; break; }
                case "Angstorm": { BaseModel.Length2 = Length.Angstorm; break; }
                case "Centimeter": { BaseModel.Length2 = Length.Centimeter; break; }
                case "Chain": { BaseModel.Length2 = Length.Chain; break; }
                case "Fathom": { BaseModel.Length2 = Length.Fathom; break; }
                case "Feet": { BaseModel.Length2 = Length.Feet; break; }
                case "Hand": { BaseModel.Length2 = Length.Hand; break; }
                case "Inch": { BaseModel.Length2 = Length.Inch; break; }
                case "Kilometer": { BaseModel.Length2 = Length.Kilometer; break; }
                case "Link": { BaseModel.Length2 = Length.Link; break; }
                case "Meter": { BaseModel.Length2 = Length.Meter; break; }
                case "Microns": { BaseModel.Length2 = Length.Microns; break; }
                case "Mile": { BaseModel.Length2 = Length.Mile; break; }
                case "Millimeter": { BaseModel.Length2 = Length.Millimeter; break; }
                case "Nanometer": { BaseModel.Length2 = Length.Nanometer; break; }
                case "Nautical Mile": { BaseModel.Length2 = Length.NauticalMile; break; }
                case "PICA": { BaseModel.Length2 = Length.PICA; break; }
                case "Rods": { BaseModel.Length2 = Length.Rods; break; }
                case "Span": { BaseModel.Length2 = Length.Span; break; }
                case "Yard": { BaseModel.Length2 = Length.Yard; break; }
                case "BTU/minute": { BaseModel.Power2 = Powers.BTUminute; break; }
                case "Foot-Pound/minute": { BaseModel.Power2 = Powers.FootPound; break; }
                case "Horsepower": { BaseModel.Power2 = Powers.Horsepower; break; }
                case "Kilowatt": { BaseModel.Power2 = Powers.Kilowatt; break; }
                case "Watt": { BaseModel.Power2 = Powers.Watt; break; }
                case "Atmosphere": { BaseModel.Pressure2 = Pressure.Atmosphere; break; }
                case "Bar": { BaseModel.Pressure2 = Pressure.Bar; break; }
                case "Kilo Pascal": { BaseModel.Pressure2 = Pressure.KiloPascal; break; }
                case "Millimeter of mercury": { BaseModel.Pressure2 = Pressure.MillimeterOfMercury; break; }
                case "Pascal": { BaseModel.Pressure2 = Pressure.Pascal; break; }
                case "Pound per square inch(PSI)": { BaseModel.Pressure2 = Pressure.PoundPerSquareInch; break; }
                case "Degree Celsius": { BaseModel.Temperature2 = Temperature.DegreeCelsius; break; }
                case "Degree Fahrenheit": { BaseModel.Temperature2 = Temperature.DegreeFahrenheit; break; }
                case "Kelvin": { BaseModel.Temperature2 = Temperature.Kelvin; break; }
                case "Day": { BaseModel.Time2 = Time.Day; break; }
                case "Hour": { BaseModel.Time2 = Time.Hour; break; }
                case "Microsecond": { BaseModel.Time2 = Time.MicroSecond; break; }
                case "Millisecond": { BaseModel.Time2 = Time.MilliSecond; break; }
                case "Minute": { BaseModel.Time2 = Time.Minute; break; }
                case "Second": { BaseModel.Time2 = Time.Second; break; }
                case "Week": { BaseModel.Time2 = Time.Week; break; }
                case "Centimeter per second": { BaseModel.Velocity2 = Velocity.CentimeterPerSecond; break; }
                case "Feet per second": { BaseModel.Velocity2 = Velocity.FeetPerSecond; break; }
                case "Kilometer per hour": { BaseModel.Velocity2 = Velocity.KilometerPerHour; break; }
                case "Knots": { BaseModel.Velocity2 = Velocity.Knots; break; }
                case "Mach(at std. atm)": { BaseModel.Velocity2 = Velocity.Mach; break; }
                case "Meter per second": { BaseModel.Velocity2 = Velocity.MeterPerSecond; break; }
                case "Miles per hour": { BaseModel.Velocity2 = Velocity.MilesPerHour; break; }
                case "Cubic centimeter": { BaseModel.Volume2 = Volume.CubicCentimeter; break; }
                case "Cubic feet": { BaseModel.Volume2 = Volume.CubicFeet; break; }
                case "Cubic inch": { BaseModel.Volume2 = Volume.CubicInch; break; }
                case "Cubic meter": { BaseModel.Volume2 = Volume.CubicMeter; break; }
                case "Cubic yard": { BaseModel.Volume2 = Volume.CubicYard; break; }
                case "Fluid ounce (UK)": { BaseModel.Volume2 = Volume.FluidOunceUK; break; }
                case "Fluid ounce (US)": { BaseModel.Volume2 = Volume.FluidounceUS; break; }
                case "Gallon (UK)": { BaseModel.Volume2 = Volume.GallonUK; break; }
                case "Gallon (US)": { BaseModel.Volume2 = Volume.GallonUS; break; }
                case "Liter": { BaseModel.Volume2 = Volume.Liter; break; }
                case "Pint (UK)": { BaseModel.Volume2 = Volume.PintUK; break; }
                case "Pint (US)": { BaseModel.Volume2 = Volume.PintUS; break; }
                case "Quart (UK)": { BaseModel.Volume2 = Volume.QuartUK; break; }
                case "Quart (US)": { BaseModel.Volume2 = Volume.QuartUS; break; }
                case "Carat": { BaseModel.Weight2 = Weight.Carat; break; }
                case "Centigram": { BaseModel.Weight2 = Weight.CentiGram; break; }
                case "Decigram": { BaseModel.Weight2 = Weight.DeciGram; break; }
                case "Dekagram": { BaseModel.Weight2 = Weight.DekaGram; break; }
                case "Gram": { BaseModel.Weight2 = Weight.Gram; break; }
                case "Hectogram": { BaseModel.Weight2 = Weight.HectoGram; break; }
                case "Kilogram": { BaseModel.Weight2 = Weight.KiloGram; break; }
                case "Long ton": { BaseModel.Weight2 = Weight.LongTon; break; }
                case "Milligram": { BaseModel.Weight2 = Weight.MilliGram; break; }
                case "Ounce": { BaseModel.Weight2 = Weight.Ounce; break; }
                case "Pound": { BaseModel.Weight2 = Weight.Pound; break; }
                case "Short ton": { BaseModel.Weight2 = Weight.ShortTon; break; }
                case "Stone": { BaseModel.Weight2 = Weight.Stone; break; }
                case "Tonne": { BaseModel.Weight2 = Weight.Tonne; break; }
            }
            GetResult();
        }

        private List<object> Maths(List<object> input)
        {
            if (input[0].ToString() == "-" || input[0].ToString() == "+" || input[0].ToString() == "/" || input[0].ToString() == "*")
            {
                input.Insert(0, 0.0);
            }
            while (input.Contains("/"))
            {
                _temp1 = ((input[input.IndexOf("/") - 1] is double) ? (double)input[input.IndexOf("/") - 1] : (Complex)(input[input.IndexOf("/") - 1])) / ((input[input.IndexOf("/") + 1] is double) ? (double)input[input.IndexOf("/") + 1] : (Complex)(input[input.IndexOf("/") + 1]));
                input.RemoveAt(input.IndexOf("/") - 1);
                input.RemoveAt(input.IndexOf("/") + 1);
                input.Insert(input.IndexOf("/"), _temp1);
                input.RemoveAt(input.IndexOf("/"));
            }
            while (input.Contains("*"))
            {
                _temp1 = ((input[input.IndexOf("*") - 1] is double) ? (double)input[input.IndexOf("*") - 1] : (Complex)(input[input.IndexOf("*") - 1])) * ((input[input.IndexOf("*") + 1] is double) ? (double)input[input.IndexOf("*") + 1] : (Complex)(input[input.IndexOf("*") + 1]));
                input.RemoveAt(input.IndexOf("*") - 1);
                input.RemoveAt(input.IndexOf("*") + 1);
                input.Insert(input.IndexOf("*"), _temp1);
                input.RemoveAt(input.IndexOf("*"));
            }
            while (input.Contains("-"))
            {
                _temp1 = (((input[input.IndexOf("-") - 1]) is double) ? (double)(input[input.IndexOf("-") - 1]) : (Complex)(input[input.IndexOf("-") - 1])) - (((input[input.IndexOf("-") + 1]) is double) ? (double)(input[input.IndexOf("-") + 1]) : (Complex)(input[input.IndexOf("-") + 1]));
                input.RemoveAt(input.IndexOf("-") - 1);
                input.RemoveAt(input.IndexOf("-") + 1);
                input.Insert(input.IndexOf("-"), _temp1);
                input.RemoveAt(input.IndexOf("-"));
            }
            while (input.Contains("+"))
            {
                _temp1 = ((input[input.IndexOf("+") - 1] is double) ? (double)input[input.IndexOf("+") - 1] : (Complex)(input[input.IndexOf("+") - 1])) + ((input[input.IndexOf("+") + 1] is double) ? (double)input[input.IndexOf("+") + 1] : (Complex)(input[input.IndexOf("+") + 1]));
                input.RemoveAt(input.IndexOf("+") - 1);
                input.RemoveAt(input.IndexOf("+") + 1);
                input.Insert(input.IndexOf("+"), _temp1);
                input.RemoveAt(input.IndexOf("+"));
            }
            return input;
        }

        private object Run(object value)
        {
            if (value.ToString().Contains("i") || value.ToString().Contains("∠"))
            {
                if (value.ToString().Contains("i") && !value.ToString().Contains("∠"))
                {
                    if (value.ToString().Split('i').Length > 2)
                    {
                        throw new InvalidOperationException("Invalid Input");
                    }
                    else
                    {
                        if (value.ToString().IndexOf("i") != value.ToString().Length - 1)
                        {
                            throw new InvalidOperationException("Input like a+bi");
                        }
                        else
                        {
                            try
                            {
                                value = new Complex(0, System.Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf("i"))));
                            }
                            catch (Exception)
                            {
                                value = new Complex(0, 1);
                            }
                        }
                    }
                }
                else if (!value.ToString().Contains("i") && value.ToString().Contains("∠"))
                {
                    if (value.ToString().Split('∠').Length > 2)
                    {
                        throw new InvalidOperationException("Invalid Input");
                    }
                    else
                    {
                        if (!double.TryParse(value.ToString().Substring(0, value.ToString().IndexOf("∠")), out tryParse) || !double.TryParse(value.ToString().Substring(value.ToString().IndexOf("∠") + 1), out tryParse))
                        {
                            throw new InvalidOperationException("Input like r∠0");
                        }
                        else
                        {
                            value = new Complex(BaseModel.MathFunctions.PolarToRectangular(System.Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf("∠"))), System.Convert.ToDouble(value.ToString().Substring(value.ToString().IndexOf("∠") + 1)), Solve)[0], BaseModel.MathFunctions.PolarToRectangular(System.Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf("∠"))), System.Convert.ToDouble(value.ToString().Substring(value.ToString().IndexOf("∠") + 1)), Solve)[1]);
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException("Invalid Input");
                }
            }
            if (value is Complex)
            {
                return (Complex)value;
            }
            if (value is double || double.TryParse(value.ToString(), out tryParse))
            {
                if (!tryParse.ToString().Contains(","))
                {
                    return tryParse;
                }
                return value.ToString();
            }
            if (value is string)
            {
                return value.ToString();
            }
            return null;
        }

        private List<Complex> GetItems(List<string> listItems)
        {
            _comp.Clear();
            foreach (string item in listItems)
            {
                if (!string.IsNullOrWhiteSpace(item) && item.Length > 0)
                {
                    inputItemText = item;
                    _items.Clear();
                    inputItemText = Regex.Replace(inputItemText, "\\+", " + ", RegexOptions.Singleline);
                    inputItemText = Regex.Replace(inputItemText, "\\-", " - ", RegexOptions.Singleline);
                    inputItemText = Regex.Replace(inputItemText, "\\*", " * ", RegexOptions.Singleline);
                    inputItemText = Regex.Replace(inputItemText, "\\/", " / ", RegexOptions.Singleline);
                    inputItemText = Regex.Replace(inputItemText, "\\(", " ( ", RegexOptions.Singleline);
                    inputItemText = Regex.Replace(inputItemText, "\\)", " ) ", RegexOptions.Singleline);
                    foreach (string item1 in inputItemText.Split(' '))
                    {
                        _items.Add(Run(item1));
                    }
                    var val = Maths(_items)[0];
                    if (val is Complex)
                    {
                        _comp.Add((Complex)val);
                    }
                    else if (val is double)
                    {
                        _comp.Add(new Complex(System.Convert.ToDouble(val), System.Convert.ToDouble(0)));
                    }
                }
                else
                {
                    throw new InvalidOperationException("Inalid Operation");
                }
            }
            return _comp;
        }

        private void TwoEquationSolve()
        {
            bool Call = true;
            try
            {
                List<String> tempList = new List<string>() { TwoA1.Text, TwoA2.Text, TwoA.Text, TwoB1.Text, TwoB2.Text, TwoB.Text };
                GetItems(tempList);
                foreach (Complex item in BaseModel.MathFunctions.TwoEquation(_comp[0], _comp[1], _comp[2], _comp[3], _comp[4], _comp[5]))
                {
                    if (Call)
                    {
                        AnsTwoX.Text = "Xa+ib = " + item.ToString();
                        Call = false;
                    }
                    else
                    {
                        AnsTwoY.Text = "Ya+ib = " + item.ToString();
                    }
                }
            }
            catch (Exception ex) { Message(ex.Message); }
        }

        private void ThreeEquationSolve()
        {
            try
            {
                List<String> tempList = new List<string>() { ThreeA1.Text, ThreeA2.Text, ThreeA3.Text, ThreeA.Text, ThreeB1.Text, ThreeB2.Text, ThreeB3.Text, ThreeB.Text, ThreeC1.Text, ThreeC2.Text, ThreeC3.Text, ThreeC.Text };
                GetItems(tempList);
                Complex[] res = BaseModel.MathFunctions.Threeequation(new Complex[] { _comp[0], _comp[1], _comp[2], _comp[3] }, new Complex[] { _comp[4], _comp[5], _comp[6], _comp[7] }, new Complex[] { _comp[8], _comp[9], _comp[10], _comp[11] });
                AnsThreeX.Text = "Xa+ib = " + res[0].ToString();
                AnsThreeY.Text = "Ya+ib = " + res[1].ToString();
                AnsThreeZ.Text = "Za+ib = " + res[2].ToString();
            }
            catch (Exception ex) { Message(ex.Message); }
        }

        #region Main methods

        void Main_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {

        }

        private void Main_Button_Click(object sender, EventArgs e)
        {
            BaseModel.Temp = null;
            BaseModel.Content = null;
            switch ((sender as Button).Text)
            {
                case "&":
                    {
                        BaseModel.Content = " & ";
                        break;
                    }
                case "^":
                    {
                        BaseModel.Content = " ^ ";
                        break;
                    }
                case "A":
                    {
                        BaseModel.Content = "A";
                        break;
                    }
                case "B":
                    {
                        BaseModel.Content = "B";
                        break;
                    }
                case "C":
                    {
                        BaseModel.Content = "C";
                        break;
                    }
                case "Inv":
                    {
                        BaseModel.Content = " Inv( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "√":
                    {
                        BaseModel.Content = " √( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "³√":
                    {
                        BaseModel.Content = " ³√( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "x√":
                    {
                        BaseModel.Content = " √( ";
                        BaseModel.Temp = ", ) ";
                        break;
                    }
                case "Log":
                    {
                        BaseModel.Content = " Log( ";
                        BaseModel.Temp = ",10 ) ";
                        break;
                    }
                case "Ln":
                    {
                        BaseModel.Content = " Ln( ";
                        BaseModel.Temp = ",e ) ";
                        break;
                    }
                case "nPr":
                    {
                        BaseModel.Content = "P";
                        break;
                    }
                case "i":
                    {
                        BaseModel.Content = "i";
                        break;
                    }
                case "Lcm":
                    {
                        BaseModel.Content = " LCM( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Abs":
                    {
                        BaseModel.Content = " Abs( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Sin":
                    {
                        BaseModel.Content = " Sin( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Cos":
                    {
                        BaseModel.Content = " Cos( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Tan":
                    {
                        BaseModel.Content = " Tan( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Sinh":
                    {
                        BaseModel.Content = " Sinh( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Cosh":
                    {
                        BaseModel.Content = " Cosh( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Tanh":
                    {
                        BaseModel.Content = " Tanh( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Mat":
                    {
                        BaseModel.Content = " Matrix ";
                        break;
                    }
                case "Prime":
                    {
                        BaseModel.Content = " Prime( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "List of prime":
                    {
                        BaseModel.Content = " ListOfPrime( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "π":
                    {
                        BaseModel.Content = "π";
                        break;
                    }
                case ",":
                    {
                        BaseModel.Content = ",";
                        break;
                    }
                case "7":
                    {
                        BaseModel.Content = "7";
                        break;
                    }
                case "8":
                    {
                        BaseModel.Content = "8";
                        break;
                    }
                case "9":
                    {
                        BaseModel.Content = "9";
                        break;
                    }
                case "4":
                    {
                        BaseModel.Content = "4";
                        break;
                    }
                case "5":
                    {
                        BaseModel.Content = "5";
                        break;
                    }
                case "6":
                    {
                        BaseModel.Content = "6";
                        break;
                    }
                case "-":
                    {
                        BaseModel.Content = " - ";
                        break;
                    }
                case "+":
                    {
                        BaseModel.Content = " + ";
                        break;
                    }
                case "0":
                    {
                        BaseModel.Content = "0";
                        break;
                    }
                case "1":
                    {
                        BaseModel.Content = "1";
                        break;
                    }
                case "2":
                    {
                        BaseModel.Content = "2";
                        break;
                    }
                case "3":
                    {
                        BaseModel.Content = "3";
                        break;
                    }
                case ".":
                    {
                        BaseModel.Content = ".";
                        break;
                    }
                case "←":
                    {

                        break;
                    }
                case "↑":
                    {

                        break;
                    }
                case "↓":
                    {

                        break;
                    }
                //One Click Events                
                case "r∠θ":
                    {

                        break;
                    }
                case "a+ib":
                    {

                        break;
                    }
                case "SD":
                    {

                        break;
                    }
                case "Var":
                    {

                        break;
                    }
                case "x̄":
                    {

                        break;
                    }
                case "x̄²":
                    {

                        break;
                    }
                case "Σx":
                    {

                        break;
                    }
                case "Σx²":
                    {

                        break;
                    }
                case "=":
                    {

                        break;
                    }
                case "%":
                    {

                        break;
                    }
                case "More":
                    {
                        BaseModel.IsDegree = Degree.Checked;
                        BaseModel.IsRadient = Radient.Checked;
                        BaseModel.IsGradient = Gradient.Checked;
                        BaseModel.QText = QText.Text;
                        OthersView();
                        return;
                    }
                case "Angle":
                    {

                        break;
                    }
                case "Clr":
                    {
                        QText.Text = null;
                        BaseModel.Content = null;
                        break;
                    }
            }
            BaseModel.StartIndex = QText.SelectionStart;
            SetContent(BaseModel.StartIndex, BaseModel.Content);
        }

        private void Main_Button_LongClick(object sender, View.LongClickEventArgs e)
        {
            BaseModel.Temp = null;
            BaseModel.Content = null;
            switch ((sender as Button).Text)
            {
                case "&":
                    {
                        BaseModel.Content = " | ";
                        break;
                    }
                case "^":
                    {
                        BaseModel.Content = " ~ ";
                        break;
                    }
                case "A":
                    {
                        BaseModel.Content = "D";
                        break;
                    }
                case "B":
                    {
                        BaseModel.Content = "E";
                        break;
                    }
                case "C":
                    {
                        BaseModel.Content = "F";
                        break;
                    }
                case "Inv":
                    {
                        BaseModel.Content = "!";
                        break;
                    }
                case "√":
                    {
                        BaseModel.Content = "²";
                        break;
                    }
                case "³√":
                    {
                        BaseModel.Content = "³";
                        break;
                    }
                case "x√":
                    {
                        BaseModel.Content = " Pow( ";
                        BaseModel.Temp = ", ) ";
                        break;
                    }
                case "Log":
                    {
                        BaseModel.Content = " Pow( 10,";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Ln":
                    {
                        BaseModel.Content = " Pow( e,";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "nPr":
                    {
                        BaseModel.Content = "Co";
                        break;
                    }
                case "r∠θ":
                    {
                        BaseModel.Content = " Pol( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "a+ib":
                    {
                        BaseModel.Content = " Rect( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "i":
                    {
                        BaseModel.Content = "∠";
                        break;
                    }
                case "Lcm":
                    {
                        BaseModel.Content = " HCF( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Abs":
                    {
                        BaseModel.Content = " Modulus( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Sin":
                    {
                        BaseModel.Content = " ASin( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Cos":
                    {
                        BaseModel.Content = " ACos( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Tan":
                    {
                        BaseModel.Content = " ATan( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Sinh":
                    {
                        BaseModel.Content = " ASinh( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Cosh":
                    {
                        BaseModel.Content = " ACosh( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Tanh":
                    {
                        BaseModel.Content = " ATanh( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Mat":
                    {
                        BaseModel.Content = " Add( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "SD":
                    {
                        BaseModel.Content = " Sub( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "Var":
                    {
                        BaseModel.Content = " Mul( ";
                        BaseModel.Temp = " ) ";
                        break;
                    }
                case "7":
                    {
                        BaseModel.Content = "M";
                        break;
                    }
                case "8":
                    {
                        BaseModel.Content = "G";
                        break;
                    }
                case "9":
                    {
                        BaseModel.Content = "T";
                        break;
                    }
                case "x̄":
                    {
                        BaseModel.Content = " ( ";
                        break;
                    }
                case "x̄²":
                    {
                        BaseModel.Content = " ) ";
                        break;
                    }
                case "4":
                    {
                        BaseModel.Content = "μ";
                        break;
                    }
                case "5":
                    {
                        BaseModel.Content = "m";
                        break;
                    }
                case "6":
                    {
                        BaseModel.Content = "k";
                        break;
                    }
                case "-":
                    {
                        BaseModel.Content = " * ";
                        break;
                    }
                case "+":
                    {
                        BaseModel.Content = " / ";
                        break;
                    }
                case "1":
                    {
                        BaseModel.Content = "f";
                        break;
                    }
                case "2":
                    {
                        BaseModel.Content = "p";
                        break;
                    }
                case "3":
                    {
                        BaseModel.Content = "n";
                        break;
                    }
                //One Click Events     
                case "Σx":
                    {
                        BaseModel.IsDegree = Degree.Checked;
                        BaseModel.IsRadient = Radient.Checked;
                        BaseModel.IsGradient = Gradient.Checked;
                        BaseModel.QText = QText.Text;
                        BaseModel.MatrixM = BaseModel.MatLists[0].GetLength(0).ToString();
                        BaseModel.MatrixN = BaseModel.MatLists[0].GetLength(1).ToString();
                        BaseModel.StatCount = BaseModel.StatLists[0].Count.ToString();
                        ListView();
                        return;
                    }
            }
            BaseModel.StartIndex = QText.SelectionStart;
            SetContent(BaseModel.StartIndex, BaseModel.Content);
        }

        #endregion

        #region Other Methods

        void Others_TextBox_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (sender == ConstantTextbox)
            {
                SetToConstant();
            }
            else
            {
                GetResult();
            }
        }

        void Others_Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (sender == ConstantSpinner)
            {
                SetToConstant();
            }
            else if (sender == ConvSpinner)
            {
                Convert_SelectionChanged();
                SetAdaptor(FromSpinner, BaseModel.Items[BaseModel.Converter]);
                SetAdaptor(ToSpinner, BaseModel.Items[BaseModel.Converter]);
            }
            else if (sender == FromSpinner)
            {
                From_SelectionChanged();
            }
            else if (sender == ToSpinner)
            {
                To_SelectionChanged();
            }
        }

        void Others_Button_Click(object sender, EventArgs e)
        {
            switch ((sender as Button).Text)
            {
                case "Back":
                    {
                        MainView();
                        break;
                    }
                case "Solve":
                    {
                        if (sender == APSolve)
                        {
                            AP_GP_HP("AP");
                        }
                        else if (sender == GPSolve)
                        {
                            AP_GP_HP("GP");
                        }
                        else if (sender == HPSolve)
                        {
                            AP_GP_HP("HP");
                        }
                        else if (sender == TwoSolve)
                        {
                            TwoEquationSolve();
                        }
                        else if (sender == ThreeSolve)
                        {
                            ThreeEquationSolve();
                        }
                        break;
                    }
            }
        }

        #endregion

        #region List methods

        void List_TextBox_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            SetVisible();
        }

        void List_Button_Click(object sender, EventArgs e)
        {
            switch ((sender as Button).Text)
            {
                case "Back":
                    {
                        BaseModel.KeyIndex = 0;
                        BaseModel.ValueIndex = 0;
                        BaseModel.StatCount = null;
                        BaseModel.MatrixM = null;
                        BaseModel.MatrixN = null;
                        MainView();
                        break;
                    }
                case "Next":
                    {
                        BaseModel.KeyIndex = Mat_StatSpinner.SelectedItemPosition;
                        BaseModel.ValueIndex = A_FSpinner.SelectedItemPosition;
                        BaseModel.StatCount = StatCount.Text;
                        BaseModel.MatrixM = MatrixM.Text;
                        BaseModel.MatrixN = MatrixN.Text;
                        ListItemsView();
                        break;
                    }
            }
        }

        private void List_Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if ((sender as Spinner).Id == Resource.Id.MatrixOrStatSpinner)
            {
                BaseModel.KeyIndex = (sender as Spinner).SelectedItemPosition;
                if (BaseModel.KeyIndex == 0)
                {
                    IsMat.Visibility = ViewStates.Visible;
                    IsStat.Visibility = ViewStates.Gone;
                }
                else
                {
                    IsMat.Visibility = ViewStates.Gone;
                    IsStat.Visibility = ViewStates.Visible;
                }
            }
            else
            {
                BaseModel.ValueIndex = (sender as Spinner).SelectedItemPosition;
            }
            MatrixM.Text = BaseModel.MatLists[BaseModel.ValueIndex].GetLength(0).ToString();
            MatrixN.Text = BaseModel.MatLists[BaseModel.ValueIndex].GetLength(1).ToString();
            StatCount.Text = BaseModel.StatLists[BaseModel.ValueIndex].Count.ToString();
            SetVisible();
        }

        #endregion

        #region ListView Methods

        void ListItems_TextBox_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            DoneFromListItems.Enabled = true;
            try
            {
                if (BaseModel.KeyIndex == 0)
                {
                    BaseModel.TempMatList[(((sender as EditText).Parent as LinearLayout).GetChildAt(0) as TextView).Id, (sender as EditText).Id] = Convert.ToDouble((sender as EditText).Text);
                }
                else
                {
                    BaseModel.TempStatList[(sender as EditText).Id] = Convert.ToDouble((sender as EditText).Text);
                }
            }
            catch (Exception)
            {
                if (BaseModel.KeyIndex == 0)
                {
                    BaseModel.TempMatList[(((sender as EditText).Parent as LinearLayout).GetChildAt(0) as TextView).Id, (sender as EditText).Id] = 0;
                }
                else
                {
                    BaseModel.TempStatList[(sender as EditText).Id] = 0;
                }
            }
            for (int i = 0; i < ListItemsGrid.ChildCount; i++)
            {
                if (ListItemsGrid.GetChildAt(i) is LinearLayout)
                {
                    for (int j = 0; j < (ListItemsGrid.GetChildAt(i) as LinearLayout).ChildCount; j++)
                    {
                        if ((ListItemsGrid.GetChildAt(i) as LinearLayout).GetChildAt(j) is EditText)
                        {
                            if (string.IsNullOrWhiteSpace(((ListItemsGrid.GetChildAt(i) as LinearLayout).GetChildAt(j) as EditText).Text))
                            {
                                DoneFromListItems.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        void ListItems_Button_Click(object sender, EventArgs e)
        {
            switch ((sender as Button).Text)
            {
                case "Back":
                    {
                        BaseModel.MatrixM = BaseModel.TempMatList.GetLength(0).ToString();
                        BaseModel.MatrixN = BaseModel.TempMatList.GetLength(1).ToString();
                        BaseModel.StatCount = BaseModel.TempStatList.Count.ToString();
                        ListView();
                        break;
                    }
                case "Done":
                    {
                        if (BaseModel.KeyIndex == 0)
                        {
                            BaseModel.MatLists[BaseModel.ValueIndex] = BaseModel.TempMatList;
                        }
                        else
                        {
                            BaseModel.StatLists[BaseModel.ValueIndex] = BaseModel.TempStatList;
                        }
                        MainView();
                        break;
                    }
                case "Close":
                    {
                        MainView();
                        break;
                    }
            }
        }

        #endregion

        #region ISerialize

        IntPtr IJavaObject.Handle
        {
            get { return this.Handle; }
        }

        void IDisposable.Dispose()
        {

        }

        #endregion

        #endregion

        #region protected methods

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            #region ContentView

            if (bundle != null)
            {
                BaseModel = (bundle.GetSerializable("Content") as MainActivity).BaseModel;
            }
            if (BaseModel.IsMain)
            {
                MainView();
            }
            else if (BaseModel.IsOthers)
            {
                OthersView();
            }
            else if (BaseModel.IsList)
            {
                ListView();
            }
            else if (BaseModel.IsListItems)
            {
                ListItemsView();
            }

            #endregion

        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutSerializable("Content", this);
        }

        #endregion

    }
}


