   M S B A M L   `   `   `  ����L  HAirUberBackoffice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:AirUberBackoffice.AlteracoesModelo"AirUberBackoffice.AlteracoesModelo  5   	   <    6AirUberBackoffice.AlteracoesModelo.EditarClienteDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:AirUberBackoffice.AlteracoesModelo     5   	   -   5          � ShowInTaskbar  .   � Title$ Editar Clientes��5	   	   $��315.483��6!   $��539.2��62   .��5
      � -   5        $��GridFormCliente��5
      $
��Left=�6"   $��10,10,9.6,0q�69   $	��328��6N   $	��Top=�6[    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��71.265 �6   �� 5      .+�5   
   �� $
��Nome��6   $
��Left=�6   $��12(�6<   $��10,10,0,10.2q�6J   $	��100��6`   $��2��6l   �� 5   
   $��Apelido��6   $��1��6"   $��0��6/   $
��Left=�6?   $��12(�6\   $��10,10,0,10.2q�6j   $	��100��6�   $��2��6�   �� 5   
   $��Ativo��6   $��2��6    $��0��6-   $
��Left=�6=   $��12(�6Z   $��10,10,0,10.2q�6h   $	��100��6~   $��2��6�   �� 5   
   $��Contacto��6   $��3��6#   $��0��60   $
��Left=�6@   $��12(�6]   $��10,9.8,0,10.6q�6k   $	��114��6�   $��2��6�   �� 5   
   $��Email��6   $��4��6    $��0��6-   $
��Left=�6=   $��12(�6Z   $��10,9.8,0,10.6q�6h   $	��114��6   $��2��6�   �� 5   
   -   5        $��TextBoxNome��5      $��0��6%   $��2��62   $
��Left=�6]   $��12(�6z   $��11.8,10,0,10.2q�6�   $	��300��6�   ��6B   ��  �� Path$
 Nome�6�   �� 5   
   -   5        $��TextBoxApelido��5      $��1��6(   $��2��65   $
��Left=�6c   $��12(�6�   $��11.8,10,0,10.2q�6�   $	��300��6�   ��6E   �� $ Apelido�6�   �� 5    
   -   5        $��TextBoxAtivo��5       $��2��6&   $��2��63   $
��Left=�6_   $��12(�6|   $��11.8,10,0,10.2q�6�   $	��300��6�   ��6C   �� $ Ativo�6�   �� 5!   
   -   5        $��TextBoxContacto��5!      $��3��6)   $��2��66   $
��Left=�6e   $��12(�6�   $��11.8,10,0,10.2q�6�   $	��300��6�   ��6F   �� $ Contacto�6�   �� 5"   
   -   5        $��TextBoxEmail��5"      $��4��6&   $��2��63   $
��Left=�6_   $��12(�6|   $��11.8,10,0,10.2q�6�   $	��300��6�   ��6C   �� $ Email�6�   �� 5$   
   $��2��6   $��11��6%    �� Orientation$ 
Horizontal=�63   $��Right=�6L   $��Bottom=�6h   $��20��6�   $��
0,0,0,-0.4q�6�   $	��142��6�   .+�5%      �� -   5         ��Name$ OkButton��5%       �� 	IsDefault . $��60��6O   $	��1,0q�6Z   $��OK��6g   �� 5&      -	   5        $ CancelButton��5&       �� IsCancel . $��60��6;   $
��10,0q�6F   $��Cancel��6o   5'      5(      5)      