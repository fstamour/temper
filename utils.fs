\ print the stack and a newline
: .ss .s cr ;

\ print a line of at least 5 "="s
\ hr stands for "horizontal rule
\ like in html
: hr ( u - ) 5 max 0 
  do [char] = emit loop ;

\ type a string and underline it with
\ "="s
\ h1 stands for "heading level 1"
\ like in html
: h1 ( c-addr u - )
  tuck 
  cr type 
  cr hr
  cr ;

\ create a u with the u-th bit set
: 1-bit-mask ( u - mask )
  1 swap lshift ;

\ create a u with u set LSB
\ e.g. 4 1s => %1111
: 1s ( u - m )
  1 swap lshift 1- ;

\ decrement the second element
: 1-' ( u1 u2 - u3 u2 )
  swap 1- swap ;

\ increment the second element
: 1+' ( u1 u2 - u3 u2 )
  swap 1+ swap ;

\ increment the value at a-addr
: 1+! ( a-addr - )
  dup @ 1+
  swap ! ;

\ fetch the u-th char from c-addr
: th-c@ ( c-addr u - c )
  chars + c@ ;

\ set the u-th char from c-addr
: th-c!  ( c c-addr u - )
  chars + c! ;

\ find the rightmost bit equals to b
: 1st-bit ( u1 b - u2 )
 {: x bit :}
 0
 begin
   dup cell #8 * <
   x 1 and bit <>
     and
 while
  1+
   x 2/ to x
 repeat
 dup #64 = if
   drop -1
 then ;

: test-1st-bit
  cr
  0 0 1st-bit .
  #2 1 1st-bit .
  #8 1 1st-bit .
  %10111 0 1st-bit .
  -1 0 1st-bit . ;

: trim-0s ( u1 - u2 u3 )
  dup 1 1st-bit
  dup >r 
  rshift
  r> ;

(
%10110 trim-0s .s
%111 trim-0s .s 
)

: trim-1s ( u1 - u2 u3 )
  dup 0 1st-bit
  dup >r 
  rshift
  r> ;

(
%10110 trim-1s .s 2drop
%111 trim-1s .s 
)

\ store a cell-counted string in the
\ dictionary
: str, ( c-addr u - )
  dup ,
  here over allot
  swap cmove ;

\ fetch a cell-counted string from
\ a-addr
: str@ ( a-addr - c-addr u )
  dup @
  swap cell+
  swap ;

\ used to skip a cell-counted string
: str+ ( a-addr - c-addr )
  dup @ + cell+ ;

\ store 2 bytes in the dictionary
: 2b, ( u - )
  dup 8 rshift c,
          $ff and c, ;

\ fetch 2 bytes at address
: 2b@ ( c-addr - u )
  dup c@ swap char+ c@
  swap 8 lshift or ;

(
here $abcd dup 2b, 
\ cr dup $8 dump
over 2b@ = .
drop
)

\ nicely print a 16bits integer
\ in binary, right-aligned, and
\ in hexadecimal, left-aligned
: .2b ( u - )
  \ left pad!
  dup log2 #17 swap - spaces
  base @ swap
  #2 base !
  dup . \ print in binary
  hex [char] $ emit . \ print in hex
  base ! ;

\ print an xt's name
: .xt-name ( xt - )
  name>string type ;