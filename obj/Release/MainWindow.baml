   M S B A M L   `   `   `  ����   ActuarialCalc    ActuarialCalc.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$   Actuarial Value Calculator��$	��408��$	��326��
������� � 
ResizeMode$ NoResize=� � Icon$ Actuarial.png��.���� $	��384��$	��Top=�$��	0,0,-6,-4q�.+��� $��Mortality Basis�� �� Left$ 81��
 �� Top$ 8��$	��165��$��	ExtraBold"�$��22(�$��Italic$��� -    ��Name$ Mortality_ComboBox��$	 142��$ 52��$	��120�� �� SelectedIndex$ 0��.@��� $��ELT15(Females).txt��$
��Left=�$	��118���� $��ELT15(Males).txt��$
��Left=�$	��118���� $��ThrowsError!��$
��Left=�$	��118���� $��Discounting Rate %��$ 10��$	 153���� -    ��Name$ DiscountRate_TextBox��$��20��$	 142�� �� TextWrapping$
 Wrap=�$��5��$	 154��$	��120��	 �� TextAlignment$	 Center=��� $��Amount Payable��$ 10��$	 179���� -   $ AmountPayable_TextBox��$��23��$	 142��$
 Wrap=�$��1��$	 179��$	��120��$	 Center=��� -   
 ��Name$
 
CurrentAge��$��Current Age��$ 10��$	 205���� -   $
 DurationOfProduct��$��Duration of Product��$ 10��$	 231���� -   $ CurrentAge_TextBox��$��23��$	 142��$
 Wrap=�$��50��$	 205��$	��120��$	 Center=��� -   $ Duration_TextBox��$��23��$	 142��$
 Wrap=�$��20��$	 234��$	��120��$	 Center=��� -   $��AnnuityButton��$ 10��$	 262��$	��115��$��40��.����  �� TextAlignment$ Center=�Calculate Annuity�� 	(Arrears)�� -	   $ AnnuityValue_TextBox��$��23��$	 142��$
 Wrap=�$	 279��$	��120��$	 Center=��� -
   $��AssuranceButton��$ 10��$	 319��$	��115��$��40��.���� $ Center=�Calculate Assurance�� 
(Discrete)�� -   $ AssuranceValue_TextBox��$��23��$	 142��$
 Wrap=�$	 336��$	��120���� -    ��Name$ AddTable_Button��$	��Add��$	 267��$ 52��$��37���� -   $ DatabaseTables_ComboBox��$	 142��$ 79��$	��120���� $��From Local Files��$ 10��$ 52��$��97���� $��From MySQL Database��$ 10��$ 75��$	��132���� -    ��Name$ UseLocalData_CheckBox��$��Use Local Data��$ 10��$	 132���� -   $ UseMySQLData_CheckBox��$��UseMySQLData��$	 110��$	 132���� -   $ Connect_Button��$
��Con.��$	 267��$ 79��$��37��