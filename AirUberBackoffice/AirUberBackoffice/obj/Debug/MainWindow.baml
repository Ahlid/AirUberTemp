   M S B A M L   `   `   `  ����L  HAirUberBackoffice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AirUberBackofficeAirUberBackoffice  5   	   "    AirUberBackoffice.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:AirUberBackoffice     5   	   -   5          � Title$$  AirUber Backoffice - Home Page��5	   	    � 	Resources 5         LabelFontStyle �� Property �� Value  TextBoxTemplate �� 
TargetType �� CornerRadius ��Name��%�  &	        &	 �     �� 5   
   .�5      ��  � ��$ Century Gothic)�63   �� 5       � ��$
 Bold"�63   �� 5       � ��
 ���F�63   5      �� 5   
   " ��62   .R�5      �� $��Border��6   
�����F�$��2q�6;   $ 5��6O   
�������
�������._�5      �� $ PART_ContentHost��6E   $��Center=�6   $��2q�6:   6E   5      5      5      .��5      ]� -   5        $��	Principal��5      .+�5   
   o� $	��Top=�6   .@�5      m� $��Ficheiro��6   .@�5      m� -   5        $��Guardar��5      6,   m� 5       -   5        $
��Sair��5       6)   5!      m� 5"      $��Viagens��6   .@�5#      m� -   5        $��Ver Viagens��5#      60   m� 5$      $��
Aeroportos��6   .@�5%      m� -   5        $��
Visualizar��5%      63   m� 5&      -   5        $��Criar��5&      6.   m� 5'      -   5        $��Editar��5'      6/   m� 5(      -	   5        $��Eliminar��5(      61   5)      m� 5*      -
   5        $��Ver Países��5*      6/   m� 5+      -   5        $��Ver Cidades��5+      60   5,      m� 5-      $��Clientes��6   .@�5.      m� -   5        $��Listar��5.      6+   m� 5/      -   5        $��Editar��5/      6+   m� 50      -   5        $��Eliminar��50      6-   51      m� 52      $��
Companhias��6   .@�53      m� -   5        $��Ver Companhias��53      63   m� 54      -   5        $��MenuItem_VerColaboradores��54      $��Ver Colaboradores��6<   6W   m� 55      -   5        $��MenuItem_VerReservas��55      $��Ver Reservas��67   6M   m� 56      -   5        $��MenuItem_VerJatos��56      $��	Ver Jatos��64   6G   m� 57      -   5        $��MenuItem_VerExtras��57      $��
Ver Extras��65   6I   58      m� 59      $��Extras��6   .@�5:      m� $��Extras��6   .@�5;      m� -   5        $��Listar��5;      6/   m� 5<      -   5        $��Criar��5<      6.   m� 5=      -   5        $��Editar��5=      6/   m� 5>      -   5        $��Eliminar��5>      61   5?      m� 5@      $��Tipo de Extras��6   .@�5A      m� -   5        $��Listar��5A      6/   m� 5B      -   5        $��Criar��5B      6.   m� 5C      -   5        $��Editar��5C      6/   m� 5D      -   5        $��Eliminar��5D      61   5E      5F      m� 5G      $��Jatos��6   .@�5H      m� $��Jatos��6   .@�5I      m� -   5        $��Listar��5I      6/   m� 5J      -   5        $��Criar��5J      6.   m� 5K      -   5        $��Editar��5K      6/   m� 5L      -   5        $��Eliminar��5L      61   5M      m� 5N      $��Modelos��6   .@�5O      m� -    5        $��Listar��5O      6/   m� 5P      -!   5        $��Criar��5P      6.   m� 5Q      -"   5        $��Editar��5Q      6/   m� 5R      -#   5        $��Eliminar��5R      61   5S      m� 5T      $��Tipo de Jatos��6   .@�5U      m� -$   5        $��Listar��5U      6/   m� 5V      -%   5        $��Criar��5V      6.   m� 5W      -&   5        $��Editar��5W      6/   m� 5X      -'   5        $��Eliminar��5X      61   5Y      5]      m� 5^      $��Ajuda��6   .@�5_      m� -(   5        $��	Ver Ajuda��5_      6.   m� 5`      -)   5        $��Sobre��5`      6*   5a      5b      �� 5d   
   $
��Left=�6   .+�5e      � -*   5        $��
FormPaises��5e      $��10q�6k    � DataContext 6%   ��  �� ElementName$ ListBoxPaises��	 �� Path$	 SelectedItem�
 � RowDefinitions
 5f      �� 5g      $��50 �6    �� 5h      $��50 �6    �� 5i      $��50 �6     � ColumnDefinitions 5k      �� 5l      $	��130 �6#   �� 5m      $	��280 �6#   �� 5n      .+�5p      �� $��Identificador:��6   $��0��61   $
��Left=�6>   $��12(�6Y   $��10q�6g   #��[  6t   �� 5q      $��Pais:��6   $��1��6(   $
��Left=�65   $��12(�6P   $��10q�6^   #��[  6k   �� 5r      -+   5        $��TextBoxPais_Id��5r      $��0��61   $��1��6>   $
��Left=�6l   $��12(�6�   $��10q�6�   $	��180��6�   {�.  #��[ 6�   ��6O   �� $	 PaisId�6�   �� 5s      -,   5        $��TextBoxPais_Nome��5s      $��1��63   $��1��6@   $
��Left=�6l   $��12(�6�   $��10q�6�   $	��180��6�   {�.  #��[ 6�   ��6Q   �� $
	 Nome�6�   �� 5t      --   5        $��ListBoxPaises��5t      $��0��6/   $��2��6<   $��3��6M   $��10.4,10,9.6,-276.8q�6^   ��5u      �� 5v      .R�5x      �� $��1q�6&   
�����F�$ 4��6R   
���������6z   �� *ActualWidth+$ ListBoxPaises��._�5y   "   �� $��	0,10,0,10q�6.   .+�5z   &   ��  �� Orientation$ 
Horizontal=�62   .+�5{   *   ��  �� TextAlignment$ Center=�65   ��6L   �� *ActualWidth+$ ListBoxPaises����6�   �� $
	 Nome�6�   5}   '   5   #   5�      5�      5�      5�      � 5�   
   -.   5        $��FormCidades��5�      $��10q�6j    6"   �� $ ListBoxCidades��$	 SelectedItem�
 5�      �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6     5�      �� 5�      $	��130 �6#   �� 5�      $	��280 �6#   �� 5�      .+�5�      �� $��Identificador:��6   $��0��6.   $
��Left=�6;   $��12(�6V   $��10q�6d   #��[  6p   �� 5�      $��Cidade:��6   $��1��6*   $
��Left=�67   $��12(�6R   $��10q�6`   #��[  6l   �� 5�      $��Pais:��6   $��2��6$   $
��Left=�61   $��12(�6L   $��10q�6Z   #��[  6f   �� 5�      -/   5        $��TextBoxCidade_Id��5�      #��[ 62   $��0��6a   $��1��6n   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6   �� $	 CidadeId�6�   �� 5�      -0   5        $��TextBoxCidade_Nome��5�      #��[ 64   $��1��6c   $��1��6p   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $
	 Nome�6�   �� 5�      -1   5        $��TextBoxCidade_NomePais��5�      #��[ 68   $��2��6g   $��1��6t   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 	Pais.Nome�6�   �� 5�      -2   5        $��ListBoxCidades��5�      $��0��60   $��2��6=   $��3��6N   $��10.4,10,9.6,-276.8q�6_   ��5�      �� 5�      .R�5�      �� $��1q�6&   
�����F�$ 4��6R   
���������6z   �� *ActualWidth+$ ListBoxPaises��._�5�   "   �� $��	0,10,0,10q�6.   .+�5�   &   �� $ 
Horizontal=�62   .+�5�   *   �� $ Center=�65   ��6L   �� *ActualWidth+$ ListBoxPaises����6�   �� $
	 Nome�6�   5�   '   5�   #   5�      5�      5�      5�      � 5�   
   -3   5        $��FormAeroportos��5�      $��10q�6o    6%   �� $ ListBoxAeroportos��$	 SelectedItem�
 5�      �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6     5�      �� 5�      $	��130 �6#   �� 5�      $	��280 �6#   �� 5�      .+�5�      �� $��Identificador:��6   $��0��61   $
��Left=�6>   $��12(�6Y   $��10q�6g   #��[  6s   �� 5�      $��
Aeroporto:��6   $��1��6-   $
��Left=�6:   $��12(�6U   $��10q�6c   #��[  6o   �� 5�      $��Cidade:��6   $��2��6*   $
��Left=�67   $��12(�6R   $��10q�6`   #��[  6l   �� 5�      $��	Latitude:��6   $��3��6,   $
��Left=�69   $��12(�6T   $��10q�6b   #��[  6n   �� 5�      $��
Longitude:��6   $��4��6-   $
��Left=�6:   $��12(�6U   $��10q�6c   #��[  6o   �� 5�      -4   5        $��TextBoxAeroporto_Id��5�      #��[ 66   $��0��6d   $��1��6q   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 CidadeId�6�   �� 5�      -5   5        $��TextBoxAeroporto_Nome��5�      #��[ 67   $��1��6f   $��1��6s   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $
	 Nome�6�   �� 5�      -6   5        $!��TextBoxAeroporto_NomeCidade��5�      #��[ 6=   $��2��6l   $��1��6y   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 Cidade.Nome�6�   �� 5�      -7   5        $��TextBoxAeroporto_Latitude��5�      #��[ 6;   $��3��6j   $��1��6w   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 Latitude�6�   �� 5�      -8   5        $ ��TextBoxAeroporto_Longitude��5�      #��[ 6<   $��4��6k   $��1��6x   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 	Longitude�6�   �� 5�      -9   5        $��ListBoxAeroportos��5�      $��0��63   $��2��6@   $��3��6Q   $��10.4,10,9.6,-276.8q�6b   ��5�      �� 5�      .R�5�      �� $��1q�6&   
�����F�$ 4��6R   
���������6z   �� *ActualWidth+$ ListBoxPaises��._�5�   "   �� $��	0,10,0,10q�6.   .+�5�   &   �� $ 
Horizontal=�62   .+�5�   *   �� $ Center=�65   ��6L   �� *ActualWidth+$ ListBoxPaises����6�   �� $
	 Nome�6�   5�   '   5�   #   5�      5�      5�      5�      � 5�   
   -:   5        $��FormReservas��5�      $��10q�6k    6#   �� $ ListBoxReservas��$	 SelectedItem�
 5�      �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6     5�      �� 5�      $	��130 �6#   �� 5�      $	��280 �6#   �� 5�      .+�5�      �� $��Identificador:��6   $��0��61   $
��Left=�6>   $��12(�6Y   $��10q�6g   #��[  6s   �� 5�      $��Data Partida:��6   $��1��60   $
��Left=�6=   $��12(�6X   $��10q�6f   #��[  6r   �� 5�      $��Data Chegada:��6   $��2��60   $
��Left=�6=   $��12(�6X   $��10q�6f   #��[  6r   �� 5�      $��Aeroporto Partida:��6   $��3��65   $
��Left=�6B   $��12(�6]   $��10q�6k   #��[  6w   �� 5�      $��Aeroporto Destino:��6   $��4��65   $
��Left=�6B   $��12(�6]   $��10q�6k   #��[  6w   �� 5�      $��Jato:��6   $��5��6(   $
��Left=�65   $��12(�6P   $��10q�6^   #��[  6j   �� 5�      $��
Companhia:��6   $��6��6-   $
��Left=�6:   $��12(�6U   $��10q�6c   #��[  6o   �� 5�      $��Cliente:��6   $��7��6+   $
��Left=�68   $��12(�6S   $��10q�6a   #��[  6m   �� 5�      $��Custo:��6   $��8��6)   $
��Left=�66   $��12(�6Q   $��10q�6_   #��[  6k   �� 5�      $��
Avaliacao:��6   $��9��6-   $
��Left=�6:   $��12(�6U   $��10q�6c   #��[  6o   �� 5�      -;   5        $��TextBoxReserva_Id��5�      #��[ 63   $��0��6b   $��1��6o   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 	ReservaId�6�   �� 5�      -<   5        $ ��TextBoxReserva_DataPartida��5�      #��[ 6<   $��1��6k   $��1��6x   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 DataPartida�6�   �� 5�      -=   5        $ ��TextBoxReserva_DataChegada��5�      #��[ 6<   $��2��6k   $��1��6x   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 DataChegada�6�   �� 5�      ->   5        $%��TextBoxReserva_AeroportoPartida��5�      #��[ 6A   $��3��6p   $��1��6}   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 AeroportoPartida.Nome�6�   �� 5�      -?   5        $%��TextBoxReserva_AeroportoDestino��5�      #��[ 6A   $��4��6p   $��1��6}   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 AeroportoDestino.Nome�6�   �� 5�      -@   5        $��TextBoxReserva_Jato��5�      #��[ 65   $��5��6d   $��1��6q   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 	Jato.Nome�6�   �� 5�      -A   5        $��TextBoxReserva_Companhia��5�      #��[ 6:   $��6��6i   $��1��6v   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 Companhia.Nome�6�   �� 5�      -B   5        $��TextBoxReserva_Cliente��5�      #��[ 68   $��7��6g   $��1��6t   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 ApplicationUserId�6�   �� 5�      -C   5        $��TextBoxReserva_Custo��5�      #��[ 66   $��8��6e   $��1��6r   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 Custo�6�   �� 5�      -D   5        $ ��TextBoxReserva_Avaliação��5�      #��[ 6:   $��9��6i   $��1��6v   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��180��6�   {�.  ��6�   �� $	 	Avaliacao�6�   �� 5�      -E   5        $��ListBoxReservas��5�      $��0��61   $��2��6>   $��3��6O   $��10.4,10,9.6,-276.8q�6`   ��5�      �� 5�      .R�5�      �� $��1q�6&   
�����F�$ 4��6R   
���������6z   �� *ActualWidth+$ ListBoxPaises��._�5�   "   �� $��	0,10,0,10q�6.   .+�5   &   �� $ 
Horizontal=�62   .+�5  *   �� $ Center=�65   ��6L   �� *ActualWidth+$ ListBoxPaises����6�   �� $	 DataChegada�6�   5  '   5  #   5     5     5	     5
     � 5  
   -F   5        $��FormClientes��5     $��10q�6k    6#   �� $ ListBoxClientes��$	 SelectedItem�
 5     �� 5     $��50 �6    �� 5     $��50 �6    �� 5     $��50 �6    �� 5     $��50 �6    �� 5     $��50 �6    �� 5     $��50 �6    �� 5     $��50 �6     5     �� 5     $	��200 �6#   �� 5     $	��300 �6#   �� 5     $	��600 �6#   �� 5     .+�5      �� $��Identificador:��6   $��0��61   $
��Left=�6>   $��12(�6Y   $��10q�6g   #��[  6s   �� 5!     $��Nome:��6   $��1��6(   $
��Left=�65   $��12(�6P   $��10q�6^   #��[  6j   �� 5"     $��Apelido:��6   $��2��6+   $
��Left=�68   $��12(�6S   $��10q�6a   #��[  6m   �� 5#     $��Data de Criação:��6   $��3��63   $
��Left=�6@   $��12(�6[   $��10q�6i   #��[  6u   �� 5$     $��Ativo:��6   $��4��6)   $
��Left=�66   $��12(�6Q   $��10q�6_   #��[  6k   �� 5%     $��	Contacto:��6   $��5��6,   $
��Left=�69   $��12(�6T   $��10q�6b   #��[  6n   �� 5&     $��Número de Créditos:��6   $��6��66   $
��Left=�6C   $��12(�6^   $��10q�6l   #��[  6x   �� 5(     -G   5        $��TextBoxCliente_Id��5(     #��[ 63   $��0��6b   $��1��6o   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��250��6�   {�.  ��6�   �� $	 ApplicationUserId�6�   �� 5)     -H   5        $��TextBoxCliente_Nome��5)     #��[ 65   $��1��6d   $��1��6q   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��250��6�   {�.  ��6�   �� $
	 Nome�6�   �� 5*     -I   5        $��TextBoxCliente_Apelido��5*     #��[ 68   $��2��6g   $��1��6t   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��250��6�   {�.  ��6�   �� $	 Apelido�6�   �� 5+     -J   5        $ ��TextBoxCliente_DataCriacao��5+     #��[ 6<   $��3��6k   $��1��6x   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��250��6�   {�.  ��6�   �� $	 DataCriacao�6�   �� 5-     -K   5        $��TextBoxCliente_Ativo��5-     #��[ 67   $��4��6e   $��1��6r   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��250��6�   {�.  ��6�   �� $	 Ativo�6�   �� 5.     -L   5        $��TextBoxCliente_Contacto��5.     #��[ 6:   $��5��6h   $��1��6u   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��250��6�   {�.  ��6�   �� $	 Contacto�6�   �� 5/     -M   5        $��TextBoxCliente_Creditos��5/     #��[ 69   $��6��6h   $��1��6u   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��250��6�   {�.  ��6�   �� $	 ContaDeCreditosId�6�   �� 50     -N   5        $��ListBoxClientes��50     $��0��61   $��2��6>   $��3��6O   $��10.4,10,9.6,-276.8q�6`   ��51     �� 52     .R�54     �� $��1q�6&   
�����F�$ 4��6R   
���������6z   �� *ActualWidth+$ ListBoxPaises��._�55  "   �� $��	0,10,0,10q�6.   .+�56  &   �� $ 
Horizontal=�62   .+�57  *   �� $ Center=�65   ��6L   �� *ActualWidth+$ ListBoxPaises����6�   �� $
	 Nome�6�   59  '   5;  #   5<     5=     5?     �� 5@     ��6   �� $	 RelativePathImagemPerfil�5A     � 5E  
   -O   5        $��FormCompanhias��5E     $��10q�6o    6%   �� $ ListBoxCompanhias��$	 SelectedItem�
 5F     �� 5G     $��50 �6    �� 5H     $��50 �6    �� 5I     $��50 �6    �� 5J     $��50 �6    �� 5K     $��50 �6    �� 5L     $��50 �6    �� 5M     $��50 �6    �� 5N     $��50 �6    �� 5O     $��50 �6    �� 5P     $��50 �6     5R     �� 5S     $	��140 �6#   �� 5T     $	��280 �6#   �� 5U     .+�5X     �� $��Identificador:��6   $��0��61   $
��Left=�6>   $��12(�6Y   $��10q�6g   #��[  6s   �� 5Y     $��Nome:��6   $��1��6(   $
��Left=�65   $��12(�6P   $��10q�6^   #��[  6j   �� 5Z     $��Morada:��6   $��2��6*   $
��Left=�67   $��12(�6R   $��10q�6`   #��[  6l   �� 5[     $��	Contacto:��6   $��3��6,   $
��Left=�69   $��12(�6T   $��10q�6b   #��[  6n   �� 5\     $��Pais:��6   $��4��6(   $
��Left=�65   $��12(�6P   $��10q�6^   #��[  6j   �� 5]     $
��Nif:��6   $��5��6'   $
��Left=�64   $��12(�6O   $��10q�6]   #��[  6i   �� 5^     $��Número de Créditos:��6   $��6��66   $
��Left=�6C   $��12(�6^   $��10q�6l   #��[  6x   �� 5_     $��Email:��6   $��7��6)   $
��Left=�66   $��12(�6Q   $��10q�6_   #��[  6l   �� 5`     $��DataCriacao:��6   $��8��6/   $
��Left=�6<   $��12(�6W   $��10q�6e   #��[  6q   �� 5a     $��Estado:��6   $��9��6*   $
��Left=�67   $��12(�6R   $��10q�6`   #��[  6l   �� 5b     -P   5        $��TextBoxCompanhia_Id��5b     #��[ 65   $��0��6d   $��1��6q   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 CompanhiaId�6�   �� 5c     -Q   5        $��TextBoxCompanhia_Nome��5c     #��[ 67   $��1��6f   $��1��6s   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $
	 Nome�6�   �� 5d     -R   5        $��TextBoxCompanhia_Morada��5d     #��[ 6:   $��2��6h   $��1��6u   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 Morada�6�   �� 5e     -S   5        $��TextBoxCompanhia_Contacto��5e     #��[ 6;   $��3��6j   $��1��6w   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 Contact�6�   �� 5f     -T   5        $��TextBoxCompanhia_Pais��5f     #��[ 67   $��4��6f   $��1��6s   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 	Pais.Nome�6�   �� 5g     -U   5        $��TextBoxCompanhia_Nif��5g     #��[ 66   $��5��6e   $��1��6r   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $		 Nif�6�   �� 5h     -V   5        $��TextBoxCompanhia_Creditos��5h     #��[ 6;   $��6��6j   $��1��6w   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $#	 ContaDeCreditos.JetCashActual�6�   �� 5i     -W   5        $��TextBoxCompanhia_Email��5i     #��[ 68   $��7��6g   $��1��6t   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 Email�6�   �� 5j     -X   5        $"��TextBoxCompanhia_DataCriacao��5j     #��[ 6>   $��8��6m   $��1��6z   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 DataCriacao�6�   �� 5k     -Y   5        $��TextBoxCompanhia_Estado��5k     #��[ 69   $��9��6h   $��1��6u   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 Estado.Nome�6�   �� 5l     -Z   5        $��ListBoxCompanhias��5l     $��0��63   $��2��6@   $��3��6Q   $��10.4,10,9.6,-276.8q�6b   ��5m     �� 5n     .R�5p     �� $��1q�6&   
�����F�$ 4��6R   
���������6z   �� *ActualWidth+$ ListBoxPaises��._�5q  "   �� $��	0,10,0,10q�6.   .+�5r  &   �� $ 
Horizontal=�62   .+�5s  *   �� $ Center=�65   ��6L   �� *ActualWidth+$ ListBoxPaises����6�   �� $
	 Nome�6�   5u  '   5w  #   5x     5y     5{     5|     � 5~  
   -[   5        $��FormColaboradores��5~     $��10q�6u    6(   �� $ ListBoxColaboradores��$	 SelectedItem�
 5     �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6     5�     �� 5�     $	��140 �6#   �� 5�     $	��280 �6#   �� 5�     .+�5�     �� $��Identificador:��6   $��0��61   $
��Left=�6>   $��12(�6Y   $��10q�6g   #��[  6s   �� 5�     $��Nome:��6   $��1��6(   $
��Left=�65   $��12(�6P   $��10q�6^   #��[  6j   �� 5�     $��Apelido:��6   $��2��6+   $
��Left=�68   $��12(�6S   $��10q�6a   #��[  6m   �� 5�     $��
Companhia:��6   $��3��6-   $
��Left=�6:   $��12(�6U   $��10q�6c   #��[  6o   �� 5�     $��Administrador:��6   $��4��61   $
��Left=�6>   $��12(�6Y   $��10q�6g   #��[  6s   �� 5�     $��Data Criação:��6   $��5��60   $
��Left=�6=   $��12(�6X   $��10q�6f   #��[  6r   �� 5�     $��Ativo:��6   $��6��6)   $
��Left=�66   $��12(�6Q   $��10q�6_   #��[  6k   �� 5�     -\   5        $��TextBoxColaborador_Id��5�     #��[ 67   $��0��6f   $��1��6s   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 ApplicationUserId�6�   �� 5�     -]   5        $��TextBoxColaborador_Nome��5�     #��[ 69   $��1��6h   $��1��6u   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $
	 Nome�6�   �� 5�     -^   5        $ ��TextBoxColaborador_Apelido��5�     #��[ 6<   $��2��6k   $��1��6x   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 Apelido�6�   �� 5�     -_   5        $"��TextBoxColaborador_Companhia��5�     #��[ 6?   $��3��6m   $��1��6z   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 Companhia.Nome�6�   �� 5�     -`   5        $&�� TextBoxColaborador_Administrador��5�     #��[ 6C   $��4��6q   $��1��6~   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 IsAdministrador�6�   �� 5�     -a   5        $$��TextBoxColaborador_DataCriacao��5�     #��[ 6@   $��5��6o   $��1��6|   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 DataCriacao�6�   �� 5�     -b   5        $��TextBoxColaborador_Ativo��5�     #��[ 6:   $��6��6i   $��1��6v   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 Ativo�6�   �� 5�     -c   5        $��ListBoxColaboradores��5�     $��0��67   $��2��6D   $��3��6U   $��10.4,10,9.6,-276.8q�6f   ��5�     �� 5�     .R�5�     �� $��1q�6&   
�����F�$ 4��6R   
���������6z   �� *ActualWidth+$ ListBoxPaises��._�5�  "   �� $��	0,10,0,10q�6.   .+�5�  &   �� $ 
Horizontal=�62   .+�5�  *   �� $ Center=�65   ��6L   �� *ActualWidth+$ ListBoxPaises����6�   �� $
	 Nome�6�   5�  '   5�  #   5�     5�     5�     5�     � 5�  
   -d   5        $��FormModelos��5�     $��10q�6i    6"   �� $ ListBoxModelos��$	 SelectedItem�
 5�     �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6     5�     �� 5�     $	��140 �6#   �� 5�     $	��280 �6#   �� 5�     .+�5�     �� $��Identificador:��6   $��0��61   $
��Left=�6>   $��12(�6Y   $��10q�6g   #��[  6s   �� 5�     $��
Tipo Jato:��6   $��1��6-   $
��Left=�6:   $��12(�6U   $��10q�6c   #��[  6o   �� 5�     $��Capacidade:��6   $��2��6.   $
��Left=�6;   $��12(�6V   $��10q�6d   #��[  6p   �� 5�     $��Alcance:��6   $��3��6+   $
��Left=�68   $��12(�6S   $��10q�6a   #��[  6m   �� 5�     $��Velocidade Máxima:��6   $��4��65   $
��Left=�6B   $��12(�6]   $��10q�6k   #��[  6w   �� 5�     $ ��Peso Máximo das Bagagens:��6   $��5��6<   $
��Left=�6I   $��12(�6d   $��10q�6r   #��[  6~   �� 5�     $��Número Motores:��6   $��6��62   $
��Left=�6?   $��12(�6Z   $��10q�6h   #��[  6t   �� 5�     $��Altitude Ideal:��6   $��7��62   $
��Left=�6?   $��12(�6Z   $��10q�6h   #��[  6t   �� 5�     $��Altura Cabine:��6   $��8��61   $
��Left=�6>   $��12(�6Y   $��10q�6g   #��[  6s   �� 5�     $��Largura Cabine:��6   $��9��62   $
��Left=�6?   $��12(�6Z   $��10q�6h   #��[  6t   �� 5�     $��Comprimento Cabine:��6   $��10��66   $
��Left=�6D   $��12(�6_   $��10q�6m   #��[  6y   �� 5�     $��
Descricao:��6   $��11��6-   $
��Left=�6;   $��12(�6V   $��10q�6d   #��[  6p   �� 5�     -e   5        $��TextBoxModelo_Id��5�     #��[ 62   $��0��6a   $��1��6n   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6   �� $	 ModeloId�6�   �� 5�     -f   5        $��TextBoxModelo_TipoJato��5�     #��[ 68   $��1��6f   $��1��6s   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 TipoJato.Nome�6�   �� 5�     -g   5        $��TextBoxModelo_Capacidade��5�     #��[ 6:   $��2��6h   $��1��6u   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 
Capacidade�6�   �� 5�     -h   5        $��TextBoxModelo_Alcance��5�     #��[ 67   $��3��6f   $��1��6s   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 Alcance�6�   �� 5�     -i   5        $$��TextBoxModelo_VelocidadeMaxima��5�     #��[ 6@   $��4��6o   $��1��6|   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 VelocidadeMaxima�6�   �� 5�     -j   5        $&�� TextBoxModelo_PesoMaximaBagagens��5�     #��[ 6B   $��5��6q   $��1��6~   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 PesoMaximaBagagens�6�   �� 5�     -k   5        $!��TextBoxModelo_NumeroMotores��5�     #��[ 6=   $��6��6l   $��1��6y   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 NumeroMotores�6�   �� 5�     -l   5        $!��TextBoxModelo_AltitudeIdeal��5�     #��[ 6=   $��7��6l   $��1��6y   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 AltitudeIdeal�6�   �� 5�     -m   5        $ ��TextBoxModelo_AlturaCabine��5�     #��[ 6<   $��8��6k   $��1��6x   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 AlturaCabine�6�   �� 5�     -n   5        $!��TextBoxModelo_LarguraCabine��5�     #��[ 6>   $��9��6l   $��1��6y   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 LarguraCabine�6�   �� 5�     -o   5        $%��TextBoxModelo_ComprimentoCabine��5�     #��[ 6A   $��10��6o   $��1��6}   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 ComprimentoCabine�6�   �� 5�     -p   5        $��TextBoxModelo_Descricao��5�     #��[ 69   $��10��6g   $��1��6u   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 	Descricao�6�   �� 5�     -q   5        $��ListBoxModelos��5�     $��0��61   $��2��6>   $��3��6O   $��10.4,10,9.6,-276.8q�6`   ��5�     �� 5�     .R�5�     �� $��1q�6&   
�����F�$ 4��6R   
���������6z   �� *ActualWidth+$ ListBoxPaises��._�5�  "   �� $��	0,10,0,10q�6.   .+�5�  &   �� $ 
Horizontal=�62   .+�5�  *   �� $ Center=�65   ��6L   �� *ActualWidth+$ ListBoxPaises����6�   �� $	 TipoJato.Nome�6�   5�  '   5�  #   5�     5�     5�     5�     � 5�  
   -r   5        $��FormTipoJatos��5�     $��10q�6m    6$   �� $ ListBoxTipoJatos��$	 SelectedItem�
 5�     �� 5�     $��50 �6    �� 5�     $��50 �6    �� 5�     $��50 �6     5�     �� 5�     $	��140 �6#   �� 5�     $	��280 �6#   �� 5�     .+�5�     �� $��Identificador:��6   $��0��61   $
��Left=�6>   $��12(�6Y   $��10q�6g   #��[  6s   �� 5�     $��Nome:��6   $��1��6(   $
��Left=�65   $��12(�6P   $��10q�6^   #��[  6j   �� 5�     -s   5        $��TextBoxTipoJato_Id��5�     #��[ 64   $��0��6b   $��1��6o   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 
TipoJatoId�6�   �� 5�     -t   5        $��TextBoxTipoJato_Nome��5�     #��[ 66   $��1��6d   $��1��6q   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $
	 Nome�6�   �� 5�     -u   5        $��ListBoxTipoJatos��5�     $��0��62   $��2��6?   $��3��6P   $��10.4,10,9.6,-276.8q�6a   ��5      �� 5     .R�5     �� $��1q�6&   
�����F�$ 4��6R   
���������6z   �� *ActualWidth+$ ListBoxPaises��._�5  "   �� $��	0,10,0,10q�6.   .+�5  &   �� $ 
Horizontal=�62   .+�5  *   �� $ Center=�65   ��6L   �� *ActualWidth+$ ListBoxPaises����6�   �� $
	 Nome�6�   5  '   5
  #   5     5     5     5     � 5  
   -v   5        $��	FormJatos��5     $��10q�6e    6    �� $ ListBoxJatos��$	 SelectedItem�
 5     �� 5     $��50 �6    �� 5     $��50 �6    �� 5     $��50 �6     5     �� 5     $	��140 �6#   �� 5     $	��280 �6#   �� 5     .+�5     �� $��Identificador:��6   $��0��61   $
��Left=�6>   $��12(�6Y   $��10q�6g   #��[  6s   �� 5     $��Nome:��6   $��1��6(   $
��Left=�65   $��12(�6P   $��10q�6^   #��[  6j   �� 5     $��Modelo:��6   $��1��6*   $
��Left=�67   $��12(�6R   $��10q�6`   #��[  6l   �� 5      $��
Companhia:��6   $��1��6-   $
��Left=�6:   $��12(�6U   $��10q�6c   #��[  6o   �� 5!     $��Em Funcionamento:��6   $��1��64   $
��Left=�6A   $��12(�6\   $��10q�6j   #��[  6v   �� 5"     $��
Aeroporto:��6   $��1��6-   $
��Left=�6:   $��12(�6U   $��10q�6c   #��[  6o   �� 5#     $��Foto:��6   $��1��6(   $
��Left=�65   $��12(�6P   $��10q�6^   #��[  6j   �� 5$     -w   5        $��TextBoxTodosJato_Id��5$     #��[ 65   $��0��6c   $��1��6p   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 JatoId�6�   �� 5%     -x   5        $��TextBoxTodosJato_Nome��5%     #��[ 67   $��1��6e   $��1��6r   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $
	 Nome�6�   �� 5&     -y   5        $��TextBoxTodosJato_Modelo��5&     #��[ 69   $��0��6g   $��1��6t   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 Modelo.Nome�6�   �� 5'     -z   5        $ ��TextBoxTodosJato_Companhia��5'     #��[ 6<   $��1��6j   $��1��6w   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 Companhia.Nome�6�   �� 5(     -{   5        $&�� TextBoxTodosJato_EmFuncionamento��5(     #��[ 6B   $��0��6p   $��1��6}   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 EmFuncionamento�6�   �� 5)     -|   5        $ ��TextBoxTodosJato_Aeroporto��5)     #��[ 6<   $��1��6j   $��1��6w   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 AeroportoNome�6�   �� 5*     -}   5        $��TextBoxTodosJato_Foto��5*     #��[ 67   $��0��6e   $��1��6r   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 RelativePathImagemPerfil�6�   �� 5+     -~   5        $��ListBoxTodosJatos��5+     $��0��63   $��2��6@   $��3��6Q   $��10.4,10,9.6,-276.8q�6b   ��5,     �� 5-     .R�5/     �� $��1q�6&   
�����F�$ 4��6R   
���������6z   �� *ActualWidth+$ ListBoxPaises��._�50  "   �� $��	0,10,0,10q�6.   .+�51  &   �� $ 
Horizontal=�62   .+�52  *   �� $ Center=�65   ��6L   �� *ActualWidth+$ ListBoxPaises����6�   �� $
	 Nome�6�   54  '   56  #   57     58     5:     5;     � 5>  
   -   5        $��FormTipoExtras��5>     $��10q�6o    6%   �� $ ListBoxTipoExtras��$	 SelectedItem�
 5?     �� 5@     $��50 �6    �� 5A     $��50 �6    �� 5B     $��50 �6     5D     �� 5E     $	��140 �6#   �� 5F     $	��280 �6#   �� 5G     .+�5I     �� $��Identificador:��6   $��0��61   $
��Left=�6>   $��12(�6Y   $��10q�6g   #��[  6s   �� 5J     $��Nome:��6   $��1��6(   $
��Left=�65   $��12(�6P   $��10q�6^   #��[  6j   �� 5K     -�   5        $��TextBoxTipoExtra_Id��5K     #��[ 65   $��0��6c   $��1��6p   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 TipoExtraId�6�   �� 5L     -�   5        $��TextBoxTipoExtra_Nome��5L     #��[ 67   $��1��6e   $��1��6r   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $
	 Nome�6�   �� 5M     -�   5        $��ListBoxTipoExtras��5M     $��0��63   $��2��6@   $��3��6Q   $��10.4,10,9.6,-276.8q�6b   ��5N     �� 5O     .R�5Q     �� $��1q�6&   
�����F�$ 4��6R   
���������6z   �� *ActualWidth+$ ListBoxPaises��._�5R  "   �� $��	0,10,0,10q�6.   .+�5S  &   �� $ 
Horizontal=�62   .+�5T  *   �� $ Center=�65   ��6L   �� *ActualWidth+$ ListBoxPaises����6�   �� $
	 Nome�6�   5V  '   5X  #   5Y     5Z     5\     5]     � 5a  
   -�   5        $��
FormExtras��5a     $��10q�6g    6!   �� $ ListBoxExtras��$	 SelectedItem�
 5b     �� 5c     $��50 �6    �� 5d     $��50 �6    �� 5e     $��50 �6    �� 5f     $��50 �6    �� 5g     $��50 �6    �� 5h     $��50 �6     5j     �� 5k     $	��140 �6#   �� 5l     $	��280 �6#   �� 5m     .+�5o     �� $��Identificador:��6   $��0��61   $
��Left=�6>   $��12(�6Y   $��10q�6g   #��[  6s   �� 5p     $��Nome:��6   $��1��6(   $
��Left=�65   $��12(�6P   $��10q�6^   #��[  6j   �� 5q     $��Tipo Extra:��6   $��2��6.   $
��Left=�6;   $��12(�6V   $��10q�6d   #��[  6p   �� 5r     $��
Companhia:��6   $��3��6-   $
��Left=�6:   $��12(�6U   $��10q�6c   #��[  6o   �� 5s     $��Valor:��6   $��4��6)   $
��Left=�66   $��12(�6Q   $��10q�6_   #��[  6k   �� 5t     -�   5        $��TextBoxExtra_Id��5t     #��[ 61   $��0��6_   $��1��6l   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6}   �� $	 ExtraId�6�   �� 5u     -�   5        $��TextBoxExtra_Nome��5u     #��[ 63   $��1��6a   $��1��6n   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6   �� $
	 Nome�6�   �� 5v     -�   5        $��TextBoxExtra_TipoExtra��5v     #��[ 68   $��2��6f   $��1��6s   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 TipoExtra.Nome�6�   �� 5w     -�   5        $��TextBoxExtra_Companhia��5w     #��[ 68   $��3��6f   $��1��6s   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 Companhia.Nome�6�   �� 5x     -�   5        $��TextBoxExtra_Valor��5x     #��[ 64   $��4��6b   $��1��6o   $
��Left=�6�   $��12(�6�   $��10q�6�   $	��200��6�   {�.  ��6�   �� $	 Valor�6�   �� 5y     -�   5        $��ListBoxExtras��5y     $��0��6/   $��2��6<   $��3��6M   $��10.4,10,9.6,-276.8q�6^   ��5z     �� 5{     .R�5}     �� $��1q�6&   
�����F�$ 4��6R   
���������6z   �� *ActualWidth+$ ListBoxPaises��._�5~  "   �� $��	0,10,0,10q�6.   .+�5  &   �� $ 
Horizontal=�62   .+�5�  *   �� $ Center=�65   ��6L   �� *ActualWidth+$ ListBoxPaises����6�   �� $
	 Nome�6�   5�  '   5�  #   5�     5�     5�     5�     5�     5�     5�     