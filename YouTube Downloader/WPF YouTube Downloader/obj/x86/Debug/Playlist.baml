   M S B A M L   `   `   `  ����  WPF YouTube Downloader%    WPF_YouTube_Downloader.Playlist   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Playlist Selector��5      .��5
      � ��5   
   �� 5       �� ImageSource$ dog.ico��6   .+�5   
   �� -   5        $��listbox1��5   l   $��Verdana)�6   $��12(�6'   $��12,73,241,87q�6V   ��65   �� *
ObsColWB+��5      �� 5      .R�5      ��  d� 	IsChecked 6   �� *	IsChecked+��6?   ��  �� Path$ Item.VideoTitle�5      5      �� 5   
   -   5        $��label1��5   e   $��Select Downloads��6   $��28��6+   $
��Left=�67   $��	12,39,0,0q�6R   $	��Top=�6s      headerTextStyle#��[  6�   � 5   
   -   5        $��grid1��5   L   $	��100��6   $
��Left=�6   $��211,-84,0,0q�67   $	��Top=�6Y   $	��200��6q   $y�Hidden=�6}   $��2��6�   5      �� 5   
   -   5        $��btnNext��5   \   $
��Next��6   $��23��6    $
��Left=�6,   $��498,240,0,0q�6G   $	��Top=�6k   $��75��6�   6�   �� 5   
   -   5        $��	btnAddAll��5   ^   $��Add All��6   $��23��6#   $
��Left=�6/   $��
388,73,0,0q�6J   $	��Top=�6o   $��75��6�   6�   �� 5   
   -   5        $��btnRemoveAll��5   b   $��
Remove All��6   $��23��6&   $
��Left=�62   $��388,113,0,0q�6M   $	��Top=�6v   $��75��6�   6�   �� 5   
   -   5        $��btnBack��5   \   $
��Back��6   $��23��6    $
��Left=�6,   $��388,240,0,0q�6G   $	��Top=�6k   $��75��6�   6�   �� 5   
   -   5        $��
lblMessage��5   y   $�� ��6   $��Center=�6   $��28��6?   $
��Left=�6K   $��	0,269,0,0q�6f   $	��Top=�6�   $	��605��6�   #��[  6�   o� 5    
   -	   5        $��menu1��5    O   $��23��6%   $��Stretch=�61   $	��Top=�6\   $��2��6t   ��6   Z� .@�5!      m� $��_File��6-   ��6   Z� .@�5"      m� -
   5        $��
mnuOptions��5"      $��_Options��6-   5#      �� 5$      m� 5%      -   5        $��mnuExit��5%      $��_Exit��6*   5&      5'      m� 5(      -   5        $��mnuAbout��5(      $��_About��6'   5)      5*      5+      5,      