\ print the stack and a newline
: .ss .s cr ;
: 'ss postpone .ss ;

\ for easy debug print
: .. dup . space ;
: '.. postpone .. ;
: .c dup emit space ;
: c? c@ . ;

\ print a line of at least 5 "="s
\ hr stands for "horizontal rule"
\ like in html
: hr ( u -- ) #5 max 0
  do [char] = emit loop ;

\ type a string and underline it with
\ "="s
\ h1 stands for "heading level 1"
\ like in html
: h1 ( c-addr u -- )
  tuck
  cr type
  cr hr
  cr ;

\ --------------------------------------------------------------------

\ create a u with the u-th bit set
: 1hot ( u -- mask )
  1 swap lshift ;

\ create a u with u set LSBs
\ e.g. 4 1s => %1111
: 1s ( u -- mask )
  1 swap lshift 1- ;

\ decrement the second element
: 1-' ( u1 u2 -- u1-1 u2 )
  swap 1- swap ;

\ increment the second element
: 1+' ( u1 u2 -- u1+1 u2 )
  swap 1+ swap ;

\ increment the value at a-addr
: 1+! ( a-addr -- )
  dup @ 1+
  swap ! ;

\ dup the next of stack
: ndup ( a b -- a a b )
  swap tuck swap ;

\ swap the next element on stack with the
\ previous one
: nswap ( a b c -- b a c )
  -rot swap rot ;

\ copy to pad
: @pad ( c-addr u -- )
  pad swap cmove ;

\ store 2 bytes in the dictionary
: 2b, ( u -- )
  dup #8 rshift c,
  $ff and c, ;

\ fetch 2 bytes at address
: 2b@ ( c-addr -- u )
  dup c@ swap char+ c@
  swap #8 lshift or ;
