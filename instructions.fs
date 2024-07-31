
: nop, 0 @assembly ;

: sleep, $9588 @assembly ;

\ Watchdog Reset
: wdr, $95A8 @assembly ;

\ Clear Carry Flag
: clc, $9488 @assembly ;
\ Set Carry Flag
: sec, $9408 @assembly ;

\ Clear Half Carry Flag
: clh, $94C8 @assembly ;
\ Set Half Carry Flag
: seh, $9458 @assembly ;

\ Clear Global Interrupt Enable bit
: cli, $94F8 @assembly ;
\ Set Global Interrupt Enable Bit
: sei, $9478 @assembly ;

\ Clear Negative Flag
: cln, $94A8 @assembly ;
\ Set  Negative Flag
: sen, $9428 @assembly ;

\ Clear Zero Flag
: clz, $9498 @assembly ;
\ Set Zero Flag
: sez, $9418 @assembly ;

\ Clear Sign Flag
: cls, $94C8 @assembly ;
\ Set Sign Flag
: ses, $9448 @assembly ;

\ Clear Overflow Flag
: clv, $94B8 @assembly ;
\ Set Overflow Flag
: sev, $9438 @assembly ;

\ Clear T Bit
: clt, $94E8 @assembly ;
\ Set T Bit
: set, $9468 @assembly ;

\ Indirect Jump
: ijmp, $9409 @assembly ;
\ Extended Indirect Jump
: eijmp, $9419 @assembly ;
\ Indirect Call to Subroutine
: icall, $9509 @assembly ;
\ Extended Indirect Call to Subroutine
: iecall, $9519 @assembly ;
\ Return from subroutine
: ret, $9508 @assembly ;
\ Return from Interrupt
: reti, $9518 @assembly ;

\ TODO lpm has 2 other variants, this one loads the value into R0
\ Load Program Memory
: lpm, $95C8 @assembly ;

\ TODO elpm has 2 other variants, this one loads the value into R0
\ Extended Load Program Memor
: elpm>r0, $95B8 @assembly ;

: rd-layout ( d r -- u )
  \ 0000 01rd dddd rrrr
  dup $f and swap \ low r
  $10 and 6 lshift \ high r
  or swap
  4 lshift ( d ) ;

\ Compare with Carry
: cpc, ( d r -- )
  \ 0000 01rd dddd rrrr
  rd-layout $0400 or @assembly ;

\ Load immediate
: ldi, ( d k -- )
  \ 1110 kkkk dddd kkkk
  dup $f and swap \ low kkkk
  $f0 and 4 lshift or \ high kkkk
  swap 16 - \ d is between 16 and 32
  4 lshift \ move d in place
  or $E000 or @assembly ;

: out, ( A d -- )
  \ 1011 1AAd dddd AAAA
  %11111 and 4 lshift swap
  dup %110000 and 5 lshift swap
  $f and or
  or $B800 or @assembly ;
