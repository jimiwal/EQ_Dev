   M S B A M L   `   `   `  ����K  GEQDevApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:EQDevApplication.ViewEQDevApplication.View  5      %    EQDevApplication.View.StockView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2vw#clr-namespace:EQDevApplication.View     5      $��Center=�5      $��Center=�5      $	��400��5	      .��5      � $��4q�6     � ColumnDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��6 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6    � 	Resources 5   
    �� DataType-  'System.Windows.Controls.ValidationError �� Path��%�   (��       5      *, +)�� 6   "  6   .R�5       �� $��Italic$�5!      
���  ��$��Right=�5#      $	��0,1q�5$      ��5%      �� $ ErrorContent�5'      5(      .+�5+   
   �� $��0��5,      $��0��6   $��Stock _type:��5-      $��Right=�5.       �� Target 5/      ��  �� ElementName$ stockTypeCmb���� 51   
   -   5         ��Name$ stockTypeCmb��52      $��0��53      $��2��6   ��54      �� $ StockTypeOptions� �� Mode$ OneTime=�	 �� SelectedItem	 55      �� $ 	StockType�
 �� ValidatesOnDataErrors
 .  =� ErrorTemplate 56      Z� �� 58   
   $��1��59      $��2��6   ��5:      �� $ stockTypeCmb��$% (Validation.Errors).CurrentItem��� 5>   
   $��2��5?      $��0��6   $��Price:��5@      $��Right=�5A       5B      �� $ priceTxt���� 5D   
   -   5         ��Name$ priceTxt��5E      $��2��5F      $��2��6   ��5G      �� $ Price�
 .  �� UpdateSourceTrigger$ PropertyChanged=� 5H      Z� �� 5J   
   $��3��5K      $��2��6   ��5L      �� $ priceTxt��$% (Validation.Errors).CurrentItem��� 5P   
   $��4��5Q      $��0��6   $��
_Quantity:��5R      $��Right=�5S       5T      �� $ quantityTxt���� 5V   
   -   5        $ quantityTxt��5W      $��4��5X      $��2��6   ��5Y      �� $ Quantity�
 . $ PropertyChanged=� 5Z      Z� �� 5\   
   $��5��5]      $��2��6   ��5^      �� $ quantityTxt��$% (Validation.Errors).CurrentItem��� 5b   
   $��7��5c      $��2��6   $��_Save��5e      $��Right=�5f      $	��4,2q�5g      $��60��5h      ��5d      �� $ SaveCommand�5h      5j      5k      