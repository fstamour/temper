(
- In this file, I'm trying to generate
  words from very high-level
  specifications.
- I call these specification "layouts"
- This is an example layout:
  s" oooo oord dddd rrrr"
- The layout is stored without spaces.
  This simplify things.
- an operand is represented by a char
- For each operand, we compute a mask
- masks are 16bits, but very few words
  assume so
)

include layout.fs
include spread.fs

\ ==========================

\ given a character and a layout,
\ compute a mask that
\ corresponds to where the
\ character appears in the layout.
: >mask ( c-addr u c - m ) -1 >spread ;

\ find the first char that is not space
: next-operand
  ( c-addr u - c|0 )
  over + swap
  do
    i c@ bl <> if
      i c@ unloop exit
    then
  loop
  0 ;

\ compute the constant part of an
\ instruction. based on the current layout
\ and provided value
: >opcode ( u -- u )
  layout' rot
  [char] o swap
  ( c-addr u u c )
  >spread ;

: pad' pad /layout ;
: next-operand' pad' next-operand ;

\ returns n masks and n
: layout>masks ( -- m* n )
  \ copy layout into pad
  layout' @pad
  \ remove the "o"s
  pad' [char] o bl replace-char
  /layout 0 do
    \ find next char or leave the loop
    next-operand' 0 = if i leave then
    \ compute the mask
    pad' next-operand' >mask
    \ remove the char
    pad' next-operand' bl replace-char
  loop ;

\ TODO factor!
\ Define an instruction
: instruction:
  ( c-addr u u "name" )
  \ store the docstring, keep its location
  \ on the return stack
  -rot here {: docstring :} str,

  \ spread and store the opcode
  \ because we need in inside the colon
  \ definition, but can't use the stack
  \ because of the colon-sys
  >opcode {: opcode :}

  \ xt that push the docstring
  :noname
  docstring ]] literal str@ ; [[
  
  \ xt that encodes the instruction
  :noname
  \ spreading each input, putting the
  \ results on the return stack
  layout>masks dup >r 0
  ?do spread postpone >r loop

  \ push the opcode
  opcode postpone literal
  \ or'ing everything
  r> 0 ?do postpone r> postpone or loop

  postpone @assembly

  postpone ;


  \ save the location of the xts (twice)
  here >r here >r
  \ store xts
  , ,
  \ store a placeholder for the "decode" xt
  0 ,

  \ finally defining the word
  :
    r> ]] literal
    mode @ cells +
    @ execute
  ; [[
  
  latestxt {: xt :}
  \ register the current word to the
  \ list of instructions
  xt @instruction

  \ xt that decodes an instruction
  \ currently returns just a boolean
  \ but I want to return a boolean, the xt
  \ the number of operands and the value of
  \ each operands
  :noname ( u - operands* xt bool )
  \ push the mask
  layout' [char] o >mask postpone literal
  postpone and
  \ push the opcode
  opcode postpone literal
  \ compare!
  postpone =
  postpone ;
  
  \ replace the placeholder
  r> decompile-mode cells +
  ( decode-xt a-addr ) !
;


