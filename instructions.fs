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