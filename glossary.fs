\ table to keep track of all
\ assembly words
create instructions #512 cells allot
variable #instruction

: 0instructions
  instructions #512 cells erase
  0 #instruction ! ;

0instructions

\ increment the number of
\ instructions
: +instruction ( - )
  #instruction 1+! ;

: @instruction ( xt - )
  instructions
  #instruction @ cells + !
  +instruction ;

\ print all instructions
: .instructions
  s" All mnemonics" h1
  #instruction @ 0 ?do
    i .
    instructions i cells + @
    .xt-name cr
  loop ;

(
' create @instruction
' + @instruction
cr
.s cr
instructions 24 dump cr
.instructions
quit
)
