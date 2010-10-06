/*
This code is derived from jgit (http://eclipse.org/jgit).
Copyright owners are documented in jgit's IP log.

This program and the accompanying materials are made available
under the terms of the Eclipse Distribution License v1.0 which
accompanies this distribution, is reproduced below, and is
available at http://www.eclipse.org/org/documents/edl-v10.php

All rights reserved.

Redistribution and use in source and binary forms, with or
without modification, are permitted provided that the following
conditions are met:

- Redistributions of source code must retain the above copyright
  notice, this list of conditions and the following disclaimer.

- Redistributions in binary form must reproduce the above
  copyright notice, this list of conditions and the following
  disclaimer in the documentation and/or other materials provided
  with the distribution.

- Neither the name of the Eclipse Foundation, Inc. nor the
  names of its contributors may be used to endorse or promote
  products derived from this software without specific prior
  written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System.Text;
using NGit.Diff;
using Sharpen;

namespace NGit.Diff
{
	public class DiffTestDataGenerator
	{
		/// <summary>Generate sequence of characters in ascending order.</summary>
		/// <remarks>
		/// Generate sequence of characters in ascending order. The first character
		/// is a space. All subsequent characters have an ASCII code one greater then
		/// the ASCII code of the preceding character. On exception: the character
		/// following which follows '~' is again a ' '.
		/// </remarks>
		/// <param name="len">length of the String to be returned</param>
		/// <returns>the sequence of characters as String</returns>
		public static string GenerateSequence(int len)
		{
			return GenerateSequence(len, 0, 0);
		}

		/// <summary>
		/// Generate sequence of characters similar to the one returned by
		/// <see cref="GenerateSequence(int)">GenerateSequence(int)</see>
		/// . But this time in each chunk of
		/// <skipPeriod> characters the last <skipLength> characters are left out. By
		/// calling this method twice with two different prime skipPeriod values and
		/// short skipLength values you create test data which is similar to what
		/// programmers do to their source code - huge files with only few
		/// insertions/deletions/changes.
		/// </summary>
		/// <param name="len">length of the String to be returned</param>
		/// <param name="skipPeriod"></param>
		/// <param name="skipLength"></param>
		/// <returns>the sequence of characters as String</returns>
		public static string GenerateSequence(int len, int skipPeriod, int skipLength)
		{
			StringBuilder text = new StringBuilder(len);
			int skipStart = skipPeriod - skipLength;
			int skippedChars = 0;
			int block = 0;
			for (int i = 0; i - skippedChars < len; ++i)
			{
				if ((i % skipPeriod) == 1)
				{
					text.Append((char)(256 + block++));
				}
				else
				{
					if (skipPeriod == 0 || i % skipPeriod < skipStart)
					{
						text.Append((char)(32 + i % 95));
					}
					else
					{
						skippedChars++;
					}
				}
			}
			return text.ToString();
		}
	}
}
