\ keep a list of masks, one for
\ each operand
create masks #255 cells allot

\ initialize masks
: 0masks 
  masks #255 cells erase ;

\ store a mask
: @mask ( c mask - )
  swap cells masks + ! ;

\ fetch the mask for the operand c
: mask@ ( c - mask )
  cells masks + @ ;

\ nicely print all stored masks
: .masks ( - )
  #255 0 do
    i cells masks + @
    dup 0<> if
      cr i emit space .2b
    else
      drop
    then
  loop
  cr ;


\ ==========================

\ given an operand (from the 
\ stack) and the stored layout.
\ compute a mask that
\ corresponds to where the
\ operand is in the layout.
\ The mask is stored directly.
:  operand+layout>mask 
  ( c - )
  {: op :}
  #15 locals| pos |
  0 \ init mask
  #16 0 do
    layout i th-c@
    op = if
      \ update mask
      pos 1-bit-mask or
    then
    pos 1- to pos
   loop
   \ store the mask for this operand
   op swap @mask ;

: test-operand+layout>mask
s" oooo oord dddd rrrr"
@layout
[char] o operand+layout>mask
[char] d operand+layout>mask
[char] r operand+layout>mask
.masks ;