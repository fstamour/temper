
0 value ramstart
0 value ramend

: attiny13
 \ 1K of program memory
 1024 0assembly
 $60 to ramstart
 $9f to ramend
 ;


attiny13