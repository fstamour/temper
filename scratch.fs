\ append new filename to the directory 
dup -rot \ copy directory size
pad swap cmove \ copy to pad area
 
( u1 c-addr u2 )
dup >r \ copy the filename size
\ fetch the directory size and 
\ keep another copy on the return stack 
rot dup >r
\ append the filename to the pad
pad + ( c-addr u2 dest ) swap cmove
pad
\ compute the new length
r> r> +

\ open
w/o ( Todo use bin instead )
create-file throw

\ write
s" hello" third write-file throw

\ close
close-file throw