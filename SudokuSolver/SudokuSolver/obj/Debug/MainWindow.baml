   M S B A M L   `   `   `  ����G  CSudokuSolver, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SudokuSolverSudokuSolver  5   	       SudokuSolver.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:SudokuSolver     5   	   -   5          � Title$  
MainWindow��5   	   $	��600��6   $	��525��6)   .��5	      � .+�5
   
      SudokuSolver.Sudoku  -   5          Name$ Sudoku��5
      $��	30,30,0,0q�6'   $
��Left=�6:   $	��Top=�6U   �� 5   
   -   5         ��Name$ 	buttSolve��5      $��
SOLVE THIS��6$   $��25(�69   $
��Bold"�6G   $
��Left=�6Y   $��
30,509,0,0q�6t   $	��Top=�6�   $	��149��6�   $��50��6�   �� 5   
   -   5        $ 	buttClean��5      $��Clean��6$   $��Right=�64   $��0q�6P   $	��Top=�6[   $��45��6s   6~   �� 5   
   -   5        $ buttFill��5      $
��Fill��6#   $
��Left=�62   $��0q�6M   $	��Top=�6X   $��27��6p   6{   �� 5   
   -   5        $ buttSave��5      {�.  $��
Save State��65   $
��Left=�6J   $��264,509,0,0q�6e   $	��Top=�6z   $��75��6�   $��22��6�   6�   �� 5   
   -   5         ��Name$ stateComboBox��5      {�.  $
��Left=�6<   $��344,537,0,0q�6W   $	��Top=�6l   $	��144��6�   6�   �� 5   
   -   5         ��Name$ txtState��5      {�.  $
��Left=�66   $��23��6Q   $��344,508,0,0q�6]    �� TextWrapping$
 Wrap=�6r   $�� ��6�   $	��Top=�6�   $	��144��6�   6�   �� 5   
   -	   5        $ 	buttUnfix��5      $��Unfix��6$   $
��Left=�64   $��	428,0,0,0q�6O   $	��Top=�6b   $��39��6z   6�   5      5      