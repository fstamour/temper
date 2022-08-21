\ buffer to store the unique 
\ characters in the layout string
\ that is currently being processed.

\ keep a list of operands (chars)
create operands #16 chars allot
variable #operand

\ initialize operands
: 0operands 
  operands #16 chars erase
  0 #operand ! ;

\ store an operand, deduplicated
: @operand ( c - )
  #16 0 do
     i chars operands + dup c@
     0= if
        c!
        #operand 1+!
        unloop exit
     then
     over swap c@ = if
       drop
       unloop exit
     then
   loop ;

: operands@ ( u - c )
  chars operands + c@ ;

\ print all stored operands
: .operands
  #operand @ 0= if
    ." No operands" cr
    exit
  then
  operands #operand  @ type ;

: test-operand
[char] o @operand
[char] o @operand
[char] r @operand
[char] d @operand
operands #16 dump ;

\ ==========================

\ extract all operands from the
\ stored layout
: layout>operands ( - )
  layout'
  0 do
    dup i chars + c@
    @operand
  loop
  drop ;

: test-layout>operands
  s" oooo oord dddd rrrr"
  @layout
  layout>operands
  #operand ? cr
  operands #16 dump ;
