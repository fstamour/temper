\ ===========================
\ buffer to assemble the machine
\ code into

\ todo "code" might have been a
\ better name??
\ instead of assembly 

variable assembly-addr
variable #assembly
variable /assembly

: 0assembly ( u - c-addr )
  \ todo warn if assembly is already
  \ allocated
  dup
  /assembly !
  allocate throw
  assembly-addr !
  0 #assembly ! ;

\ todo word to allot a buffer 
\ instead of allocate it

\ get the address of the buffer
: assembly ( - c-addr )
  assembly-addr @ ;

\ free the assembly buffer
: free-assembly ( - )
  assembly free throw
  0 assembly !
  0 #assembly !
  0 /assembly ! ;
 
\ get the address and length of the
\ buffer
: assembly' ( - c-addr u )
  assembly 
  #assembly @ 2* chars ;

\ store 2 bytes in the buffer
: @assembly ( u - )
  \ todo maybe check if assembly
  \ is allocated
  \ todo check for overflow
  dup #8 rshift swap
  $ff and swap
  ( lsb msb )
  assembly' +
  ( msb lsb c-addr )
  tuck
  ( msb c-addr lsb c-addr ) !
  char+ !
  #assembly 1+! ;

\ get the u-th machine code
: assembly@ ( u - u )
  2* chars assembly +
  2b@ ;

: .assembly ( - )
  \ todo definitely check if 
  \ assembly is allocated
  assembly' dump ;

(
#10 0assembly
$abcd @assembly
.assembly 
quit
)