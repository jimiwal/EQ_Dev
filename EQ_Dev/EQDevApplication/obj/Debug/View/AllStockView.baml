   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5      K GEQDevApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:EQDevApplication.ConverterEQDevApplication.Converter 5      (   "EQDevApplication.View.AllStockView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      5      9conv(clr-namespace:EQDevApplication.Converter      5        � 	Resources  5	      <  6EQDevApplication.Converter.TransiactionalCostConverter   TransiactionalCostConverter9  3EQDevApplication.Converter.FundSummaryTextConverter  FundSummaryConverter  StockGroups �� Source �� Path �� GroupDescriptions2  ,System.Windows.Data.PropertyGroupDescription   PropertyName �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction(  "System.Windows.Controls.GroupStyle  StockGroupStyle   HeaderTemplate	 �� Orientation
 ��Name  Brush_HeaderBackground �� Padding �� 	Converter �� ConverterParameter �� RelativeSource �� AncestorType �� AncestorLevel �� Triggers �� Value �� Binding �� 
TargetName �� Property �� Value  StockItemStyle �� 
TargetType �� Mode �� Triggers ]� 
Conditions �� Property �� Value��%�  &	        &	      &	 &     &	     7  7  7  7  &	 �      5
   
   6+     5   
   6(   �� 5   
    5      �� $ 	AllStocks� 5        5      $ 	StockType��6+    5        5      $ Quantity��6&   $ 
Descending=�6>     5      $ Price��6&   $ 	Ascending=�6;   5        5   
    5      �� 5      .R�5      �� $	 
Horizontal=�6!   .+�5      �� $	
 txt��5      8��  $
��Bold"�5       
�������$��0q�5"      $ 4,2,0,2q�5#      $��Equity Stocks :��5$      �� 5&      $
 
txtSummary��5'      8�� $
��Bold"�5)      
�������$��0q�5+      $ 4,2,5,2q�5,      ��5-      �� *DataContext+8  $ Equity�� �� *FindAncestor+" >�$ 1��53       54      �� 55      $
 Bond��6D    6&   �� $
 Name�.a�56      �� $	 txt��6%    � ��$ Bond Stocks :��6F   �� 57      $ 
txtSummary��6%    6M   �� *DataContext+8  $
 Bond�� �� *FindAncestor+" >�$ 1�� � ��5=      5@      5B      �� 5D   
   " ��6'   .�5I      ��  � ��$ Stretch=�6;   �� 5N       �@��
IsSelected 6+   �� $ 
IsSelected�$ TwoWay=� 5O      ]� 5P       5Q      �� 5R       �@��AlternationIndex$ 1��6M   �� 5S       �@��
IsSelected .  6:   �� 5T       �@J�IsMouseOver .  6;   ./�5V      ��  � �� �	#EEEEEEEE63   5W      5Y      5Z      .��5\      ]� .+�5]   
   � $��Bottom=�6   $��0,2,4,2q�6'   .+�5^      �� $��Right=�6   $	 
Horizontal=�65   $��Center=�6N   .+�5_      �� $��Fund summary - ��6   �� 5`       �� ContentStringFormat$ c��5b      ��5a      �� $ TotalSelectedSummary�5b      5d      5e      �� 5g   
    �� AlternationCount$ 2��5h       � DataContext# [ 5i      #��[ 5j      ��5k      ��   �� 
GroupStyle  5m      �� 5n      ! �� ResourceKey$! StockGroupStyle��5o      " �� View" 5s      �� 5t      .P�5u      �� $
��Name��5v      # �� CellTemplate# 5x      �� 5y      .R�5z   "   �� $
 
txtNameBox��6,   ��6@   �� $ DisplayName���5{   &   �� 5|   *   " ��60    5}   .   �� 5~   2    6>   �� *	Content+# [   �� *FindAncestor+" �� True5�   >   .a�5�   6   ��  � ��
 �  ��6S   5�   3   5�   +   5�   #   5�      5�      �� 5�      $��Price��5�      $ �� DisplayMemberBinding$ 5�      �� $ Price��� 5�      $��Quantity��5�      $ 5�      �� $ Quantity��� 5�      $��MarketValue��5�      $ 5�      �� $ MarketValue��� 5�      $��TransactionCost��5�      $ 5�      �� $ TransactionCost��� 5�      $��StockWeight��5�      $ 5�      �� $ StockWeight�5�      5�      5�      5�      