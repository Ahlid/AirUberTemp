   M S B A M L   `   `   `  ����L  HAirUberBackoffice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:AirUberBackoffice.AlteracoesModelo"AirUberBackoffice.AlteracoesModelo  5   	   ?    9AirUberBackoffice.AlteracoesModelo.EditarAeroportosDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:AirUberBackoffice.AlteracoesModelo     5   	   -   5          � ShowInTaskbar  .   � Title$ Editar Aeroporto��5	   	   $��263.662��6"   $��534.59��63   .��5
      � -   5        $��GridFormAeroporto��5
      $
��Left=�6$   $��10q�6;    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��71.265 �6   �� 5      .+�5   
   �� $
��Nome��6   $
��Left=�6   $��12(�6<   $��10,10,0,10.2q�6J   $	��100��6`   $��2��6l   �� 5   
   $��Cidade��6   $��1��6!   $��0��6.   $
��Left=�6>   $��12(�6[   $��10,10,0,10.2q�6i   $	��100��6   $��2��6�   �� 5   
   $��Latitude��6   $��2��6#   $��0��60   $
��Left=�6@   $��12(�6]   $��10,10,0,10.2q�6k   $	��100��6�   $��2��6�   �� 5   
   $��	Longitude��6   $��3��6$   $��0��61   $
��Left=�6A   $��12(�6^   $��10,9.8,0,10.6q�6l   $	��114��6�   $��2��6�   �� 5   
   -   5        $��TextBoxNome��5      $��0��6%   $��2��62   $
��Left=�6]   $��12(�6z   $��11.8,10,0,10.2q�6�   $	��300��6�   ��6B   ��  �� Path$
 Nome�6�   �� 5   
   -   5        $��TextBoxCidade��5      $��1��6'   $��2��64   $
��Left=�6c   $��12(�6�   $��11.8,10,0,10.2q�6�   $	��300��6�   ��6D   �� $ CidadeId�6�   �� 5   
   -   5        $��TextBoxLatitude��5      $��2��6)   $��2��66   $
��Left=�6e   $��12(�6�   $��11.8,10,0,10.2q�6�   $	��300��6�   ��6F   �� $ Latitude�6�   �� 5   
   -   5        $��TextBoxLongitude��5      $��3��6*   $��2��67   $
��Left=�6g   $��12(�6�   $��11.8,10,0,10.2q�6�   $	��300��6�   ��6G   �� $ 	Longitude�6�   �� 5!   
   $��2��6   $��11��6%    �� Orientation$ 
Horizontal=�63   $��Right=�6L   $��Bottom=�6h   $��20��6�   $��
0,0,0,-0.4q�6�   $	��142��6�   .+�5"      �� -   5         ��Name$ OkButton��5"       �� 	IsDefault . $��60��6O   $	��1,0q�6Z   $��OK��6g   �� 5#      -   5        $ CancelButton��5#       �� IsCancel . $��60��6;   $
��10,0q�6F   $��Cancel��6o   5$      5%      5&      