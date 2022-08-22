\ instructions with 0 operands
s" Return from subroutine"
  $9508 i0 ret,
s" Clear Carry Flag"
  $9488 i0 clc,
s" Clear Half Carry Flag"
  $94D8 i0 clh,
s" Clear Global Interrupt Enable bit"
  $94F8 i0 cli,

( 
all the instructions without
operands:
ijmp [ ] eijmp [ ]
icall [ ] eicall [ ]
ret [x] reti [ ]
elmp [ ]
sec [ ] clc [x]
sen [ ] cln
sez [ ] clz [ ]
sei cli [x]
ses [ ] cls [ ]
sev [ ] clv [ ]
set [ ] clt [ ]
seh [ ] clh [x]
break [ ]
nop [ ]
sleep [ ]
wdr [ ]
)

\ instructions grouped by layout
(
oooo oord dddd rrrr
0001 11 adc
0000 11 add
0010 00 and

oooo oooo kkdd kkkk
1001 0110 adiw
\ cbr is a subset of adiw
1001 1000 cbi

oooo kkkk dddd kkkk
0111 andi

oooo oood dddd oooo
1001 010 0101 asr

oooo oooo osss oooo
1001 0100 1 1000 bclr
\ cbr is a subset of blcr
1001 0100 0 1000 bset



oooo oood dddd obbb
1111 100 0 bld
1111 101 0 bst

oooo ookk kkkk ksss
1111 01 brbc
1111 00 brbs

\ The following instructions are
\ equivalent to brbc or brbs with
\ specific values of s

brbc
====
1111 01 000 brcc brsh
1111 01 001 brne
1111 01 011 brvs
1111 01 010 brpl
1111 01 100 brge
1111 01 101 brhc
1111 01 110 brtc
1111 01 111 brid

brbs
====
1111 00 000 brcs brlo
1111 00 001 breq 
1111 00 010 brmi
1111 00 011 brvc
1111 00 100 brlt
1111 00 101 brhs
1111 00 110 brts
1111 00 111 brie

\ 32bits instructions!!!
oooo oook kkkk oook
kkkkk kkkk kkkk kkkkk
1001 010 111 call
1001 010 110 jmp

oooo oood dddd oooo
kkkkk kkkk kkkk kkkkk
1001 000 0000 lds
1001 001 0000 sts
)





